using Newtonsoft.Json;

namespace MeDirectTest.Models
{
    public class RateModel
    {
        [JsonProperty("odata.metadata")]
        public string Date { get; set; }
        public string Historical { get; set; }
        public double Result { get; set; }
        public bool Success { get; set; }
        public Info Info { get; set; }
        public Query Query { get; set; }
    }

    public class Info
    {
        public double Rate{ get; set; }
        public long Timestamp { get; set; }
    }

    public class Query
    {
        public double Amount { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
}
