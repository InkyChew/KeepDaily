namespace DomainLayer.Models
{
    public class LineNotifySetting
    {
        public string Client_ID { get; set; } = null!;
        public string Client_Secret { get; set; } = null!;
        public string Redirect_URI { get; set; } = null!;
    }
}
