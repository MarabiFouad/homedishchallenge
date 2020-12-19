using HomeDishTest.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace HomeDishTest.Services
{
    public class HomeDishApiService : IBasketService
    {
        private string BasketUrl = "/products";
        private readonly IHttpClientFactory httpFactory;
        public HomeDishApiService(IHttpClientFactory httpFactory)
        {
            this.httpFactory = httpFactory;
        }


        public async Task<Basket> GetBasketAsync(CancellationToken cancellationToken)
        {
            var http = httpFactory.CreateClient("apiservice");
            var response = await http.GetAsync(BasketUrl, cancellationToken);
            
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<Basket>(responseStream,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true },
                    cancellationToken);
            }

            return default;
        }

    }
}
