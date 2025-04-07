using LR_CiT8.Models;
using LR_CiT8.Services;
using Microsoft.AspNetCore.Mvc;
using LR_CiT8.Utils;

namespace LR_CiT8.Controllers
{
    [Route("api/currency")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly CurrencyService _currencyService;
        private readonly HistoryService _historyService;

        public CurrencyController(CurrencyService currencyService, HistoryService historyService)
        {
            _currencyService = currencyService;
            _historyService = historyService;

        }

        [HttpPost("convert")]
        public async Task<IActionResult> CurrensyConvert([FromBody] CurrencyConvert result)
        {
            decimal rate = await _currencyService.GetExchangeRateAsync(result.From, result.To, result.Amount);

            _historyService.Add(new ConversionHistory
            {
                Type = "currency",
                From = result.From,
                To = result.To,
                Value = (double)result.Amount,
                Result = (double)rate,
                Timestamp = DateTime.Now

            });

            return Ok(new { result = $"{result.Amount} {result.From} = {rate} {result.To}" });
        }

        [HttpGet("history")]
        public IActionResult GetCurrencyHistory([FromQuery] string format = "json")
        {
            var data = _historyService.GetAll().Where(h => h.Type == "currency").ToList();

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
