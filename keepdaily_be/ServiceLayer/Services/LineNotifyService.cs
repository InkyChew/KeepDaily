using DomainLayer.Models;
using Microsoft.Extensions.Configuration;
using ServiceLayer.IServices;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ServiceLayer.Services
{
    public class LineNotifyService : ILineNotifyService
    {
        private const string LineAuthorizeAPI = "https://notify-bot.line.me/oauth/authorize";
        private const string LineTokenAPI = "https://notify-bot.line.me/oauth/token";
        private const string LineNotifyAPI = "https://notify-api.line.me/api/notify";
        private const string LineRevokeAPI = "https://notify-api.line.me/api/revoke";

        private readonly IConfiguration _configuration = null!;
        private readonly LineNotifySetting _lineNotifySetting;
        private readonly IHttpClientFactory _httpClientFactory;

        public LineNotifyService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _lineNotifySetting = new()
            {
                Client_ID = _configuration["LineNotifySetting:client_id"] ?? throw new NullReferenceException("Null LineNotifySetting:client_id"),
                Client_Secret = _configuration["LineNotifySetting:client_secret"] ?? throw new NullReferenceException("Null LineNotifySetting:client_secret"),
                Redirect_URI = _configuration["LineNotifySetting:redirect_uri"] ?? throw new NullReferenceException("Null LineNotifySetting:auth_redirect_uri")
            };
            _httpClientFactory = httpClientFactory;
        }

        public string GetAuthorizationUrl(string email)
        {
            var URL = LineAuthorizeAPI;
            URL += "?response_type=code";
            URL += $"&client_id={_lineNotifySetting.Client_ID}";
            URL += $"&redirect_uri={_lineNotifySetting.Redirect_URI}";
            URL += "&scope=notify";
            URL += $"&state={email}";
            URL += "&response_mode=form_post";
            return URL;
        }

        public async Task<string> PostTokenAsync(string code)
        {
            HttpClient client = _httpClientFactory.CreateClient();
            client.Timeout = new TimeSpan(0, 0, 60);

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("redirect_uri", _lineNotifySetting.Redirect_URI),
                new KeyValuePair<string, string>("client_id", _lineNotifySetting.Client_ID),
                new KeyValuePair<string, string>("client_secret", _lineNotifySetting.Client_Secret)
            });

            var httpResMsg = await client.PostAsync(LineTokenAPI, content);
            var resContent = await httpResMsg.Content.ReadAsStringAsync();
            var res = JsonSerializer.Deserialize<LineTokenRes>(resContent) ?? throw new Exception("LineTokenRes parse error.");
            if (httpResMsg.IsSuccessStatusCode) return res.AccessToken!;
            else throw new Exception(res.Message);
        }

        public async Task<HttpResponseMessage> SendAsync(string token, LineSendInfo info)
        {
            HttpClient client = _httpClientFactory.CreateClient();
            client.Timeout = new TimeSpan(0, 0, 60);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var uri = $"{LineNotifyAPI}?message={info.Message}";
            if (info.StickerPackageId != null) uri = $"{uri}&stickerPackageId={info.StickerPackageId}";
            if (info.StickerId != null) uri = $"{uri}&stickerId={info.StickerId}";

            var content = new MultipartFormDataContent();
            if (info.ImageFile != null)
            {
                if (info.ImageFile.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await info.ImageFile.CopyToAsync(ms);
                        var baContent = new ByteArrayContent(ms.ToArray());
                        content.Add(baContent, "imageFile", info.ImageFile.FileName);
                    }
                }
            }
            else
            {
                content = null;
            }

            return await client.PostAsync(uri, content);
        }
    }
}
