using TrafficLight.Options;

namespace TrafficLight.Models
{
    public interface IConfigurationProvider
    {
        TrafficLightOptions GetTrafficLightOptions();
    }
}
