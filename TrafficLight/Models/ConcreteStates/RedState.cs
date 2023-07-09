namespace TrafficLight.Models.ConcreteStates
{
    public class RedState : TrafficLightState
    {
        private readonly IConfigurationProvider _configurationProvider;
        public override string Color => "Red";

        public RedState(IConfigurationProvider configurationProvider, int minSeconds) : base(1000)
        {
            _configurationProvider = configurationProvider;
            _counter = minSeconds;
        }

        public override void HandleTransition()
        {
            int redAndYellowStateMinSeconds = 5;
            _trafficLight.TransitionTo(new RedAndYellowState(_configurationProvider, redAndYellowStateMinSeconds));

            Console.WriteLine("TrafficLight: Red");
        }
    }
}
