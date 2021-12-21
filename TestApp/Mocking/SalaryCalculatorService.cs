using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Mocking
{
    public interface IHttpClient
    {
        Task<T> GetFromJsonAsync<T>(string requestUri);
    }

    public interface IRateService
    {
        Task<Rate> GetAsync(string currencyCode);
    }

    public class NbpRateService : IRateService
    {
        const string url = "api/exchangerates/tables/a/?format=json";

        private readonly HttpClient client;

        public NbpRateService(HttpClient client)
        {
            this.client = client;
        }
        

        public async Task<Rate> GetAsync(string currencyCode)
        {
            var rates = await client.GetFromJsonAsync<RatesList[]>(url);

            Rate rate = rates.SelectMany(p => p.rates).SingleOrDefault(r => r.code == currencyCode);

            return rate;
        }
    }

    public class StandardHttpClient : IHttpClient
    {
        public async Task<T> GetFromJsonAsync<T>(string requestUri)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://api.nbp.pl/");

            var result = await  client.GetFromJsonAsync<T>(requestUri);

            return result;
        }
    }

    public class SalaryCalculatorService
    {
        private readonly IRateService rateService;

        public SalaryCalculatorService(IRateService rateService)
        {
            this.rateService = rateService;
        }

        public SalaryCalculatorService()
            : this(new NbpRateService(new HttpClient()))
        {
        }
     
        public async Task<decimal> CalculateAsync(decimal amount, string currencyCode = "PLN")
        {
            Rate rate = await rateService.GetAsync(currencyCode);

            decimal result = amount * (decimal)rate.mid;

            return result;
        }
    }


    public class RatesList
    {
        public string table { get; set; }
        public string no { get; set; }
        public string effectiveDate { get; set; }
        public Rate[] rates { get; set; }
    }

    public class Rate
    {
        public string currency { get; set; }
        public string code { get; set; }
        public float mid { get; set; }
    }
}
