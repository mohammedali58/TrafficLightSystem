namespace TrafficLight.Models
{
    public interface ITrafficLight : IDisposable
    {
        void Start();
        void TransitionTo(TrafficLightState state);
        void PadestrianButtonPushed();
        TrafficLightState GetCurrentTrafficLightState();

    }
}
