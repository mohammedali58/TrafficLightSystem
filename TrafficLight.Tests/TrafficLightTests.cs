using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Moq;
using TrafficLight.Models;
using TrafficLight.Services;

namespace TrafficLight.Tests
{
    public class TrafficLightTests //: IClassFixture<Models.TrafficLight>
    //public class TrafficLightTests : IClassFixture<WebApplicationFactory<Startup>>
    {

        private readonly Mock<IHubContext<NotificationHub, INotificationHub>> _mockHub;
        private readonly Mock<IConfigurationProvider> _configurationProvider;

        public TrafficLightTests()
        {
            _mockHub = new Mock<IHubContext<NotificationHub, INotificationHub>>();
            //_mockHub.Setup(a=> a.Groups).Returns(new )
            _configurationProvider = new Mock<IConfigurationProvider>();
            _configurationProvider
                .Setup(a => a.GetTrafficLightOptions())
                .Returns(new Options.TrafficLightOptions
                {
                    GreenMaxSeconds = "360",
                    GreenMinSeconds = "120",
                    RedMinSeconds = "120"
                });
        }

        [Fact]
        public void Start_ShouldInitializeTheCurrentStateToRed()
        {
            // Arrange
            var trafficLight = new Models.TrafficLight(_mockHub.Object, _configurationProvider.Object);

            // Act
            trafficLight.Start();

            // Assert
            Assert.IsType(
                trafficLight.GetCurrentTrafficLightState().GetType(),
                new TrafficLight.Models.ConcreteStates.RedState(_configurationProvider.Object, 10));
        }

        [Fact]
        public void TransitionTo_ShouldChangeTheCurrentStateToRedAndYellow()
        {
            // Arrange
            var trafficLight = new Models.TrafficLight(_mockHub.Object, _configurationProvider.Object);

            // Act
            trafficLight.Start();
            trafficLight.TransitionTo(new Models.ConcreteStates.RedAndYellowState(_configurationProvider.Object));

            // Assert
            Assert.IsType(
                trafficLight.GetCurrentTrafficLightState().GetType(),
                new Models.ConcreteStates.RedAndYellowState(_configurationProvider.Object));

        }

        [Fact]
        public void TransitionTo_ShouldChangeTheCurrentStateToGreen()
        {
            // Arrange
            var trafficLight = new Models.TrafficLight(_mockHub.Object, _configurationProvider.Object);

            // Act
            trafficLight.Start();
            trafficLight.TransitionTo(new TrafficLight.Models.ConcreteStates.RedAndYellowState(_configurationProvider.Object, 5));
            trafficLight.TransitionTo(new TrafficLight.Models.ConcreteStates.GreenState(_configurationProvider.Object, 10, 15));

            // Assert
            Assert.IsType(
                trafficLight.GetCurrentTrafficLightState().GetType(),
                new TrafficLight.Models.ConcreteStates.GreenState(_configurationProvider.Object, 10, 15));

        }

        [Fact]
        public void GetCurrentTrafficLightState_ShouldReturnTheCurrentState()
        {
            // Arrange
            var trafficLight = new Models.TrafficLight(_mockHub.Object, _configurationProvider.Object);

            // Act
            trafficLight.Start();
            var currentState = trafficLight.GetCurrentTrafficLightState();

            // Assert
            Assert.IsType(
                currentState.GetType(),
                new TrafficLight.Models.ConcreteStates.RedState(_configurationProvider.Object, 10));
        }

    }
}