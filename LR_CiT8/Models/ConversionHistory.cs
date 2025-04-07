namespace LR_CiT8.Models
{
    public class ConversionHistory
    {
        public string Type { get; set; } // "currency" или "unit"
        public string From { get; set; }
        public string To { get; set; }
        public double Value { get; set; }
        public double Result { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
