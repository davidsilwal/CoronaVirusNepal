using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CovidNepalVisualization
{
    public class WorldMeter
    {
        [JsonProperty("updated")]
        public long Updated { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }


        [JsonProperty("cases")]
        public long Cases { get; set; }

        [JsonProperty("todayCases")]
        public long TodayCases { get; set; }

        [JsonProperty("deaths")]
        public long Deaths { get; set; }

        [JsonProperty("todayDeaths")]
        public object TodayDeaths { get; set; }

        [JsonProperty("recovered")]
        public long Recovered { get; set; }

        [JsonProperty("active")]
        public long Active { get; set; }

        [JsonProperty("critical")]
        public object Critical { get; set; }

        [JsonProperty("casesPerOneMillion")]
        public long CasesPerOneMillion { get; set; }

        [JsonProperty("deathsPerOneMillion")]
        public double DeathsPerOneMillion { get; set; }

        [JsonProperty("tests")]
        public long Tests { get; set; }

        [JsonProperty("testsPerOneMillion")]
        public long TestsPerOneMillion { get; set; }

        [JsonProperty("population")]
        public long Population { get; set; }

        [JsonProperty("continent")]
        public string Continent { get; set; }

        [JsonProperty("activePerOneMillion")]
        public double ActivePerOneMillion { get; set; }

        [JsonProperty("recoveredPerOneMillion")]
        public double RecoveredPerOneMillion { get; set; }

        [JsonProperty("criticalPerOneMillion")]
        public object CriticalPerOneMillion { get; set; }

        public DateTime GetUpdatedDateTime() => (new DateTime(1970, 1, 1)).AddMilliseconds(Updated);
    }

    public partial class WorldMeterRecord
    {
        [JsonProperty("updated")]
        public long Updated { get; set; }

        [JsonProperty("cases")]
        public long Cases { get; set; }

        [JsonProperty("todayCases")]
        public long TodayCases { get; set; }

        [JsonProperty("deaths")]
        public long Deaths { get; set; }

        [JsonProperty("todayDeaths")]
        public long TodayDeaths { get; set; }

        [JsonProperty("recovered")]
        public long Recovered { get; set; }

        [JsonProperty("active")]
        public long Active { get; set; }

        [JsonProperty("critical")]
        public long Critical { get; set; }

        [JsonProperty("casesPerOneMillion")]
        public double CasesPerOneMillion { get; set; }

        [JsonProperty("deathsPerOneMillion")]
        public double DeathsPerOneMillion { get; set; }

        [JsonProperty("tests")]
        public long Tests { get; set; }

        [JsonProperty("testsPerOneMillion")]
        public double TestsPerOneMillion { get; set; }

        [JsonProperty("population")]
        public long Population { get; set; }

        [JsonProperty("continent")]
        public string Continent { get; set; }

        [JsonProperty("activePerOneMillion")]
        public double ActivePerOneMillion { get; set; }

        [JsonProperty("recoveredPerOneMillion")]
        public double RecoveredPerOneMillion { get; set; }

        [JsonProperty("criticalPerOneMillion")]
        public double CriticalPerOneMillion { get; set; }

        public DateTime GetDateTime() => (new DateTime(1970, 1, 1)).AddMilliseconds(Updated);
    }

    public class WorldMeterDataService
    {
        private readonly HttpClient _httpClient;

        public WorldMeterDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async ValueTask<WorldMeter> GetSummaryByCountryAsync(string country)
        {
            var response = await _httpClient.GetFromJsonAsync<WorldMeter>($"/v2/countries/{country}?yesterday=false&strict=false&allowNull=false");
            return response;
        }

        public async ValueTask<WorldMeterRecord> GetSummaryByContientAsync(string continent)
        {
            var response = await _httpClient.GetFromJsonAsync<WorldMeterRecord>($"/v2/continents/{continent}?yesterday=false&strict=false&allowNull=false");
            return response;
        }

        public async ValueTask<WorldMeterRecord> GetGlobalByContientAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<WorldMeterRecord>($"/v2/all?yesterday=false&allowNull=false");
            return response;
        }
    }
}
