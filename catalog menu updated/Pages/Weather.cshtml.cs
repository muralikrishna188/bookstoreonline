using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using catalog.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace catalog.Pages
{
    public class WeatherModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public WeatherModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public void OnPostWeather()
        {
            var ip = Request.Form["ip"];
            ViewData["ip"] = ip;
            var client = new HttpClient();
            var address = $"http://{ip}/api/weather";
            try
            {
                var resp = client.GetAsync(address).Result;
                ViewData["weather"] = resp.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                ViewData["error"] = "Error getting weather data: " + ex.Message;
            }
        }
    }
}
