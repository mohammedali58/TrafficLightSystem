namespace TrafficLight.Models.ConcreteStates
{
    public class YellowState : TrafficLightState
    {
        private readonly IConfigurationProvider _configurationProvider;
        public override string Color => "Yellow";

        public YellowState(IConfigurationProvider configurationProvider) : base(1000)
        {
            _configurationProvider = configurationProvider;
            _counter = 5;
        }
        public override void HandleTransition()
        {
            var redMinSeconds = int.Parse(_configurationProvider.GetTrafficLightOptions().RedMinSeconds);
            _trafficLight.TransitionTo(new RedState(_configurationProvider, redMinSeconds));
            Console.WriteLine("TrafficLight: Yellow");
        }
    }
}
