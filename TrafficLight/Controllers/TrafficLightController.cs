using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TrafficLight.Models;
using TrafficLight.Services;

namespace TrafficLight.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrafficLightController : ControllerBase
    {
        private ITrafficLight _trafficLight;
        private readonly Models.IConfigurationProvider _configurationProvider;
        private readonly IHubContext<NotificationHub, INotificationHub> _hubContext;

        public TrafficLightController(
            IHubContext<NotificationHub, INotificationHub> hubContext,
            ITrafficLight trafficLight,
            Models.IConfigurationProvider configurationProvider)
        {
            _hubContext = hubContext;
            _trafficLight = trafficLight;
            _configurationProvider = configurationProvider;
        }

        [HttpGet("Start")]
        public IActionResult Start()
        {
            _trafficLight?.Start();
            return Ok();
        }

        [HttpGet("PushPadestrianButton")]
        public IActionResult PushPadestrianButton()
        {
            _trafficLight.PadestrianButtonPushed();
            return Ok();
        }

        [HttpGet("GetCurrentConfiguration")]
        public IActionResult GetCurrentConfiguration()
        {            
            return Ok(_configurationProvider.GetTrafficLightOptions());
        }

    }
}
