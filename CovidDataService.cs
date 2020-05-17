using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CovidNepalVisualization
{

    public class CountryModel
    {

        //    "Confirmed": 0,
        //"Deaths": 0,
        //"Recovered": 0,
        //"Active": 0,
        //"Date": "2020-01-23T00:00:00Z"
        public int Confirmed { get; set; }
        public int Deaths { get; set; }
        public int Recovered { get; set; }
        public int Active { get; set; }
        public DateTimeOffset Date { get; set; }
    }


    public class ChartViewModel
    {
        public string Confirmed { get; set; }
        public string Deaths { get; set; }
        public string Recovered { get; set; }
        public string Active { get; set; }
        public string Date { get; set; }

        public int TotalConfirmed { get; set; }
        public int TotalDeaths { get; set; }
        public int TotalRecovered { get; set; }
        public int TotalActive { get; set; }
    }

    public class Summary
    {
        public Global Global { get; set; }
        public List<Countries> Countries { get; set; }

    }

    public class Global
    {
        public int NewConfirmed { get; set; }
        public int TotalConfirmed { get; set; }
        public int NewDeaths { get; set; }
        public int TotalDeaths { get; set; }
        public int NewRecovered { get; set; }
        public int TotalRecovered { get; set; }

    }

    public class Countries
    {
        public string Country { get; set; }
        public int NewConfirmed { get; set; }
        public int TotalConfirmed { get; set; }
        public int NewDeaths { get; set; }
        public int TotalDeaths { get; set; }
        public int NewRecovered { get; set; }
        public int TotalRecovered { get; set; }
        public DateTimeOffset Date { get; set; }
    }

    public class CovidDataService
    {
        private readonly HttpClient _httpClient;

        public CovidDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async ValueTask<ChartViewModel> GetDataByNepalAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<CountryModel>>("/country/np");


            var result = new ChartViewModel {
                Confirmed = JsonConvert.SerializeObject(response.Select(r => r.Confirmed).ToList()),
                Deaths = JsonConvert.SerializeObject(response.Select(r => r.Deaths).ToList()),
                Recovered = JsonConvert.SerializeObject(response.Select(r => r.Recovered).ToList()),
                Active = JsonConvert.SerializeObject(response.Select(r => r.Active).ToList()),
                Date = JsonConvert.SerializeObject(response.Select(r => r.Date).ToList()),
                TotalConfirmed = response.Sum(s => s.Confirmed),
                TotalDeaths = response.Sum(s => s.Deaths),
                TotalRecovered = response.Sum(s => s.Recovered),
                TotalActive = response.Sum(s => s.Active),
            };
            return result;

        }

        public async ValueTask<(string Confirmed, string Deaths, string Recovered, string Active)> GetConfirmedTimeSeries()
        {
            var response = await _httpClient.GetFromJsonAsync<List<CountryModel>>("/country/np");

            var Confirmed = "[";
            var Deaths = "[";
            var Recovered = "[";
            var Active = "[";

            foreach (var item in response)
            {
                Confirmed += ($" [Date.UTC({item.Date.Year}, {item.Date.Month}, {item.Date.Day}), {item.Confirmed}],");
                Deaths += ($" [Date.UTC({item.Date.Year}, {item.Date.Month}, {item.Date.Day}), {item.Deaths}],");
                Recovered += ($" [Date.UTC({item.Date.Year}, {item.Date.Month}, {item.Date.Day}), {item.Recovered}],");
                Active += ($" [Date.UTC({item.Date.Year}, {item.Date.Month}, {item.Date.Day}), {item.Active}],");
            }
            Confirmed += "]";
            Deaths += "]";
            Recovered += "]";
            Active += "]";

            return (Confirmed, Deaths, Recovered, Active);
        }

        public async ValueTask<Summary> GetSummaryAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<Summary>("/summary");
            return response;

        }

    }
}
