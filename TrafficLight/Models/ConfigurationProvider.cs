using Microsoft.Extensions.Options;
using TrafficLight.Options;

namespace TrafficLight.Models
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        private readonly IOptionsMonitor<TrafficLightOptions> _options;

        public ConfigurationProvider(IOptionsMonitor<TrafficLightOptions> options)
        {
            _options = options;
        }
        public TrafficLightOptions GetTrafficLightOptions()
        {
            return _options.CurrentValue;
        }
    }
}
