using Microsoft.AspNetCore.Http;

namespace DomainLayer.Models
{
    public class LineSendInfo
    {
        public string Message { get; set; } = null!;
        public IFormFile? ImageFile { get; set; }
        public string? StickerPackageId { get; set; }
        public string? StickerId { get; set; }
    }
}
