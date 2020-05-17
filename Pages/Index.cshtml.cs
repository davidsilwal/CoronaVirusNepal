using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace CovidNepalVisualization.Pages
{

    public class ChartViewModel
    {
        public string Deaths { get; set; }
        public string Confirmed { get; set; }
        public string Recovered { get; set; }
    }
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostTimeSeriesByCountryNameAsync([FromServices]JHUDataservice dataservice, string country)
        {
            var result = await dataservice.GetConfirmedTimeSeries(country);

            var response = new ChartViewModel { 
                Confirmed = result.Confirmed,
                Deaths = result.Deaths,
                Recovered = result.Recovered

            };

            //return response;

            return new OkObjectResult(response);

        }
    }

}
