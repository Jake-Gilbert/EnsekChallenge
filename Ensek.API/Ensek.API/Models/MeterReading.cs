namespace Ensek.API.Entities
{
    public class MeterReading
    {
        public int AccountId { get; set; }
        public string MeterReadingDateTime { get; set; }
        public string MeterReadValue { get; set; }
        public MeterReading(int accountId, string meterReadingDateTime, string meterReadValue)
        {
            AccountId = accountId;
            MeterReadingDateTime = meterReadingDateTime;
            MeterReadValue = meterReadValue;
        }
        
        
    }
}
