using Ensek.API.Entities;
using System.Net;
using System.Text;

namespace Ensek.API.Helpers
{
    public class FileReader
    {      
        public IList<MeterReading> ParseToMeterReadings(IFormFile csv)
        {
            IList<MeterReading> meterReadings = new List<MeterReading>();

            using (var reader = new StreamReader(csv.OpenReadStream()))
            {
                reader.ReadLine();
                var line = string.Empty;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split(",");

                    meterReadings.Add(new MeterReading(int.Parse(values[0]), values[1], values[2]));
                }
            }
            return meterReadings;
        }
    }
}
