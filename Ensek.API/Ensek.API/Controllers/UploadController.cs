using Ensek.API.Entities;
using Ensek.API.Helpers;
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
           IList<MeterReading> validReadings = validator.Validate(meterReadings);

           DatabaseHelper dbController = new DatabaseHelper();
           string source = "Data Source = C:\\Users\\Jake\\source\\repos\\Jake-Gilbert\\EnsekChallenge\\Ensek.db";
            //C:\Users\Jake\source\repos\Jake-Gilbert\EnsekChallenge
            IList<int> existingAccountIds =  dbController.GetAccountIds(source);
           dbController.UploadValidData(source, validReadings, existingAccountIds);


            return Ok("5 good 2 bad");
        }             
    }
}
