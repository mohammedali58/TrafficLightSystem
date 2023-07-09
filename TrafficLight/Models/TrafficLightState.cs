using System.Timers;

namespace TrafficLight.Models
{
    public abstract class TrafficLightState : IDisposable
    {
        protected TrafficLight _trafficLight;
        protected System.Timers.Timer _timer;
        protected int _counter = 0;


        public int SecondsElapsed => _counter;

        public abstract string Color { get; }
        public abstract void HandleTransition();

        public TrafficLightState(double interval)
        {
            _timer = new System.Timers.Timer(interval);
            _timer.Start();
            _timer.Elapsed += new ElapsedEventHandler(OnTimeElapsed);
        }

        public void SetTrafficLight(TrafficLight trafficLight)
        {
            _trafficLight = trafficLight;
        }

        void OnTimeElapsed(object source, ElapsedEventArgs e)
        {

            _counter--;

            if (_counter == 0)
                HandleTransition();

            _trafficLight.GetCurrentTrafficLightState();

            Console.WriteLine($"Counter : {_counter}");
        }

        public virtual void PadestrianButtonPushed()
        {
            Console.WriteLine("Nothing should happen");
        }

        public void Dispose()
        {
            _timer.Stop();
            _timer.Dispose();
        }
    }
}
