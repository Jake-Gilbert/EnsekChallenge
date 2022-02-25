using Ensek.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Ensek.API.Helpers
{
    public class CsvValidator
    {
        public int ValidReadings { get; set; } = 0;
        public int InvalidReadings { get; set; } = 1;
        public IList<MeterReading> Validate(IList<MeterReading> meterReadings)
        {
            IList<MeterReading> validMeters = new List<MeterReading>();
            HashSet<int> accountIds = new HashSet<int>();
            foreach (MeterReading meterReading in meterReadings) {
                try
                {
                    if (meterReading != null && !accountIds.Contains(meterReading.AccountId))
                    {
                        if (meterReading.MeterReadValue.Length == 5)
                        {
                            validMeters.Add(meterReading);
                            accountIds.Add(meterReading.AccountId);
                            ValidReadings++;
                        }
                    }                   
                }
                catch
                {
                    InvalidReadings++;
                    continue;
                }
            }
            return validMeters;
        }
    }
}
