namespace MeDirectAssessment.Models
{

    public class TransactionModel
    {
        public string TransactionId { get; set; }
        public string TrClientId { get; set; }
        public string TrFirstName { get; set; }
        public string TrLastName { get; set; }
        public string TrFromCurrency { get; set; }
        public double TrFromAmount { get; set; }
        public string TrToCurrency { get; set; }
        public double TrRate { get; set; }
        public double TrResult { get; set; }
        public DateTime TrRateTimestamp { get; set; }
        public DateTime TransactionTimestamp { get; set; }
    }
}
