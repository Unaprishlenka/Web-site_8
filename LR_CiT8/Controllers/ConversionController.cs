using Microsoft.AspNetCore.Mvc;
using LR_CiT8.Models;
using LR_CiT8.Services;
using LR_CiT8.Utils;

namespace LR_CiT8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversionController : ControllerBase
    {
        private readonly HistoryService _historyService;
        public ConversionController(HistoryService historyService)
        {
            _historyService = historyService;
        }

        [HttpPost("convert/units")]
        public IActionResult ConvertUnits([FromBody] ConversionRequest request)
        {
            double[,] conversionMatrix = new double[,]
            {
                // мили        км       м       футы
                {   1,         1.61,   1609.34,  5280    },//мили
                { 0.621,        1,     1000,     3280.84 },//км
                { 0.00062137,  0.001,    1,      3.281   },//м
                { 0.000528,   0.000328, 0.3048,  1      },//футы

            };

            Unit from = Enum.Parse<Unit>(request.From);
            Unit to = Enum.Parse<Unit>(request.To);
            double result = request.Value * conversionMatrix[(int)from, (int)to];

            _historyService.Add(new ConversionHistory
            {
                Type = "unit",
                From = request.From,
                To = request.To,
                Value = (double)request.Value,
                Result = (double)result,
                Timestamp = DateTime.Now
            });

            return Ok(new { result = $"{request.Value} {request.From} = {result} {request.To}" });
        }

        [HttpGet("units/history")]
        public IActionResult GetUnitHistory([FromQuery] string format = "json")
        {
            var data = _historyService.GetAll().Where(h => h.Type == "unit").ToList();

            if(format == "xml")
            {
                return new ContentResult
                {
                    ContentType = "text/xml",
                    Content = XmlConverter.ToXml(data)
                };
            }
            return Ok(data);
        }

    }
}
