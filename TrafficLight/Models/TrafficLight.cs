using Microsoft.AspNetCore.SignalR;
using TrafficLight.Models.ConcreteStates;
using TrafficLight.Services;

namespace TrafficLight.Models
{
    public class TrafficLight : ITrafficLight
    {
        protected TrafficLightState _currentState;
        private readonly IHubContext<NotificationHub, INotificationHub> _hubContext;
        private readonly IConfigurationProvider _configurationProvider;

        public TrafficLight(IHubContext<NotificationHub, INotificationHub> hubContext,
            IConfigurationProvider configurationProvider)
        {
            _hubContext = hubContext;
            _configurationProvider = configurationProvider;
        }
        public void Start()
        {
            var redMinSeconds = int.Parse(_configurationProvider.GetTrafficLightOptions().RedMinSeconds);
            _currentState = new RedState(_configurationProvider, redMinSeconds);
            _currentState.SetTrafficLight(this);

            GetCurrentTrafficLightState();
        }

        public void TransitionTo(TrafficLightState state)
        {
            _currentState.Dispose();
            _currentState = state;
            _currentState.SetTrafficLight(this);
        }

        public TrafficLightState GetCurrentTrafficLightState()
        {
            _hubContext?.Clients?.All?.Send(_currentState);
            return _currentState;
        }

        public void PadestrianButtonPushed()
        {
            _currentState.PadestrianButtonPushed();
        }

        public void Dispose()
        {
            _currentState.Dispose();
        }
    }
}
