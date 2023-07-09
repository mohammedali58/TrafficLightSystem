using TrafficLight.Models;

namespace TrafficLight.Services
{
    public interface INotificationHub
    {
        public Task Send(TrafficLightState trafficLightState);
    }
}
