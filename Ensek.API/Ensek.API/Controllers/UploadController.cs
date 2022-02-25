using Ensek.API.Entities;
using Ensek.API.Helpers;
using Ensek.API.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Ensek.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UploadController : Controller
    {
        [HttpPost(Name = "meter-reading-uploads")]
        public IActionResult Upload ( IFormFile csv)
        {
           var fileReader = new FileReader();
           IList<MeterReading> meterReadings = fileReader.ParseToMeterReadings(csv);
           CsvValidator validator = new CsvValidator();
           IList<MeterReading> passedReadings = validator.Validate(meterReadings);

           DatabaseHelper dbController = new DatabaseHelper();
           string source = "Data Source = C:\\Users\\Jake\\source\\repos\\Jake-Gilbert\\EnsekChallenge\\Ensek.db";
           IList<int> existingAccountIds =  dbController.GetAccountIds(source);
            int[] SuccessfulAndFailed = dbController.UploadValidData(source, passedReadings, existingAccountIds);

            return Ok(new UploadResponse
            {
                SuccessfulReadings = SuccessfulAndFailed[0],
                FailedReadings = validator.InvalidReadings + SuccessfulAndFailed[1]
            });
        }             
    }
}
