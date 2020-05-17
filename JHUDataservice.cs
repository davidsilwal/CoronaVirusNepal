using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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
    }
}
