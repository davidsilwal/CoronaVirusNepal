using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CovidNepalVisualization
{
    public class JHUDataservice
    {
        private readonly HttpClient _httpClient;

        public JHUDataservice(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async ValueTask<(string Confirmed, string Deaths, string Recovered)> GetConfirmedTimeSeries(string country = "nepal")
        {
            var response = await _httpClient.GetFromJsonAsync<JhuModel>($"/v2/historical/{country}?lastdays=all");

            var Confirmed = "[";
            var Deaths = "[";
            var Recovered = "[";

            foreach (var item in response.Timeline.Cases)
            {
                var date = DateTime.Parse(item.Key);
                Confirmed += ($" [Date.UTC({date.Year}, {date.Month}, {date.Day}), {item.Value}],");
            }

            foreach (var item in response.Timeline.Deaths)
            {
                var date = DateTime.Parse(item.Key);
                Deaths += ($" [Date.UTC({date.Year}, {date.Month}, {date.Day}), {item.Value}],");
            }

            foreach (var item in response.Timeline.Recovered)
            {
                var date = DateTime.Parse(item.Key);

                Recovered += ($" [Date.UTC({date.Year}, {date.Month}, {date.Day}), {item.Value}],");
            }

            Confirmed += "]";
            Deaths += "]";
            Recovered += "]";

            return (Confirmed, Deaths, Recovered);
        }
    }
}
