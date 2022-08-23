using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IIRS.Models.ViewModel;
using IIRS.Utilities.Common;
using IIRS.Utilities.SignalRHelper;
using IIRS.Utilities.SwaggerHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace IIRS.Controllers
{
    [ApiController]
    [CustomRoute]
    [Produces("application/json")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IHubContext<MessageHub> _hubContext;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public WeatherForecastController(IHubContext<MessageHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpGet]
        [ServiceFilter(typeof(UseLogAttribute))]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost]
        public async Task<IActionResult> Send(string message)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", message);
            return Ok();
        }
    }
}
