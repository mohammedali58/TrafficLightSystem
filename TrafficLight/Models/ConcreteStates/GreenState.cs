namespace TrafficLight.Models.ConcreteStates
{
    public class GreenState : TrafficLightState
    {
        private readonly IConfigurationProvider configurationProvider;
        private readonly int _maxSeconds;
        public override string Color => "Green";

        public GreenState(IConfigurationProvider configurationProvider, int minSeconds, int maxSeconds)
            : base(1000)
        {
            this.configurationProvider = configurationProvider;
            _maxSeconds = maxSeconds;
            _counter = minSeconds;
        }

        public override void HandleTransition()
        {
            _trafficLight.TransitionTo(new YellowState(this.configurationProvider));

            Console.WriteLine("TrafficLight: Green");
        }
        public override void PadestrianButtonPushed()
        {
            if (_counter + 30 >= _maxSeconds)
            {
                _counter = _maxSeconds;
            }
            else
            {
                _counter += 30;
            }
        }
    }
}
