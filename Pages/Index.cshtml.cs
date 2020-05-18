using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
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
        private readonly WorldMeterDataService _worldMeterDataService;

        public IndexModel(ILogger<IndexModel> logger, WorldMeterDataService worldMeterDataService)
        {
            _logger = logger;
            _worldMeterDataService = worldMeterDataService;
        }

        public async Task OnGetAsync()
        {
            var result = await TryFethWorldMetersData();
        }

        private string[] Countries()
        {
            return new string[] { "nepal", "india", "usa", "asia", "australia" };
        }


        public ValueTask<List<WorldMeter>> TryFethWorldMetersData()
        {
            var result = new List<WorldMeter>();
            Parallel.ForEach(Countries(), async (country) => {
                var metric = await _worldMeterDataService.GetSummaryByCountryAsync(country);
                result.Add(metric);
            });
            return new ValueTask<List<WorldMeter>>(result);
        }

        public ICollection<WorldMeter> WorldMetersData { get; set; }

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
