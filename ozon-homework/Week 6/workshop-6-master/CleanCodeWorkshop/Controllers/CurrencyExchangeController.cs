﻿using Dapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Npgsql;

namespace CleanCodeWorkshop.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class CurrencyExchangeController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<decimal>> Post([FromBody] ExchangeRequest request)
        {
            var cs = @"Server=127.0.0.1:5432;Database=CleanCodeWorkshop;User ID=postgres;Password=qwerty";

            using var con = new NpgsqlConnection(cs);

            con.Open();
            var q1 = "select rate from exchange_rates where currency_code='" + request.FromCurrency + "'";
            var q2 = "select rate from exchange_rates where currency_code='" + request.ToCurrency + "'";

            if (request.ConvertOnDate != null)
            {
                q1 = q1 + " and date = '" + request.ConvertOnDate.Value.Date.ToString("yyyy-MM-dd")+"';";
                q2 = q2 + " and date = '" + request.ConvertOnDate.Value.Date.ToString("yyyy-MM-dd")+"';";
            }
            else
            {
                q1 = q1 + " and date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "';";
                q2 = q2 + " and date = '" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "';";
            }

            var val1 = con.ExecuteScalar<decimal?>(q1);
            var val2 = con.ExecuteScalar<decimal?>(q2);

            if (val1 != null && val2 != null)
            {
                return val2 * request.Value / val1;
            }

            var clnt = new HttpClient();

            if (request.ConvertOnDate == null)
            {
                var c = await clnt.GetAsync(
                    "https://openexchangerates.org/api/latest.json?app_id=");
                var data = await c.Content.ReadAsStringAsync();
                var j_converted = JsonConvert.DeserializeObject<Rtes>(data);
                if (request.FromCurrency == "USD")
                    if (request.ToCurrency == "USD")
                        return request.Value;

                if (request.FromCurrency == "USD")
                    return request.Value / j_converted.Rates[request.FromCurrency];

                if (request.FromCurrency == "USD")
                    return j_converted.Rates[request.ToCurrency] * request.Value;

                var res = j_converted.Rates[request.ToCurrency] * request.Value / j_converted.Rates[request.FromCurrency];

                return res;
            }
            else
            {
                var c = await clnt.GetAsync(
                    "https://openexchangerates.org/api/historical/"+ request.ConvertOnDate.Value.Date.ToString("yyyy-MM-dd") + ".json?app_id=d5bb3be26a7e409f9908eb8b9c9cd9e8");
                var data = await c.Content.ReadAsStringAsync();
                var j_converted = JsonConvert.DeserializeObject<Rtes>(data);
                if (request.FromCurrency == "USD")
                    if (request.ToCurrency == "USD")
                        return request.Value;

                if (request.FromCurrency == "USD")
                    return request.Value / j_converted.Rates[request.FromCurrency];

                if (request.FromCurrency == "USD")
                    return j_converted.Rates[request.ToCurrency] * request.Value;

                var res = j_converted.Rates[request.ToCurrency] * request.Value / j_converted.Rates[request.FromCurrency];

                return res;
            }
        }
    }

    public class ExchangeRequest
    {
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public decimal Value { get; set; }
        public DateTime? ConvertOnDate { get; set; }
    }

    public class Rtes
    {
        public Dictionary<string, decimal> Rates { get; set; }
    }
}
