namespace MeDirectTest.Models
{
    public class RateRequestModel
    {
        public string ClientIdRequest { get; set; }
        public string From{ get; set; }
        public string To { get; set; }
        public double Amount { get; set; }
        public DateTime? Date { get; set; }
    }
}
