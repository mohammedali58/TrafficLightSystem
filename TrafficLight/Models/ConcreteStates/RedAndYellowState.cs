namespace TrafficLight.Models.ConcreteStates
{
    public class RedAndYellowState : TrafficLightState
    {
        private readonly IConfigurationProvider _configurationProvider;
        public override string Color => "Red And Yellow";

        public RedAndYellowState(IConfigurationProvider configurationProvider, int minSeconds = 5) : base(1000)
        {
            _configurationProvider = configurationProvider;
            _counter = minSeconds;
        }


        public override void HandleTransition()
        {
            var greenMinSeconds = int.Parse(_configurationProvider.GetTrafficLightOptions().GreenMinSeconds);
            var greenMaxSeconds = int.Parse(_configurationProvider.GetTrafficLightOptions().GreenMaxSeconds);
            _trafficLight.TransitionTo(new GreenState(_configurationProvider, greenMinSeconds, greenMaxSeconds));

            Console.WriteLine("TrafficLight: RED AND YELLOW");
        }
    }
}
