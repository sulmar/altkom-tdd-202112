using FluentAssertions;
using NUnit.Framework;
using System;
using TestApp.Mocking;

namespace TestApp.UnitTests
{
    public class FakeValidFile : IFileReader
    {
        public string ReadAllText(string path) => "{'Latitude': 52.01, 'Longitude': 28.99 }";
    }

    public class FakeInvalidFile : IFileReader
    {
        public string ReadAllText(string path) => "a";
    }

    public class FakeEmptyFile : IFileReader
    {
        public string ReadAllText(string path) => string.Empty;
    }

    public class TrackingServiceTests
    {      

        [Test]
        public void Get_ValidJson_ShouldReturnsLocation()
        {
            // Arrange
            IFileReader fileReader = new FakeValidFile();
            TrackingService trackingService = new TrackingService(fileReader);

            // Act
            Location location = trackingService.Get();

            // Assert
            location.Latitude.Should().Be(52.01);
            location.Longitude.Should().Be(28.99);
        }

        [Test]
        public void Get_InvalidJson_ShouldThrowApplicationException()
        {
            // Arrange
            IFileReader fileReader = new FakeInvalidFile();
            TrackingService trackingService = new TrackingService(fileReader);

            // Act
            Action act = ()=> trackingService.Get();

            // Assert
            act.Should().Throw<ApplicationException>();
        }

        [Test]
        public void Get_EmptyJson_ShouldThrowApplicationException()
        {
            // Arrange
            IFileReader fileReader = new FakeEmptyFile();
            TrackingService trackingService = new TrackingService(fileReader);

            // Act
            Action act = () => trackingService.Get();

            // Assert
            act.Should().Throw<ApplicationException>();
        }
    }
}
