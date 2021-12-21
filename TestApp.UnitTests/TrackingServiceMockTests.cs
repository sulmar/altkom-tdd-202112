using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using TestApp.Mocking;

namespace TestApp.UnitTests
{
    // Install-Package Moq
    public class TrackingServiceMockTests
    {
        private Mock<IFileReader> mockFileReader;
        private TrackingService trackingService;

        private const string ValidJson = "{'Latitude': 52.01, 'Longitude': 28.99 }";
        private const string InvalidJson = "a";

        [SetUp]
        public void Setup()
        {
            mockFileReader = new Mock<IFileReader>(MockBehavior.Strict);
            trackingService = new TrackingService(mockFileReader.Object);
        }

       [Test]
        public void Get_ValidJson_ShouldReturnsLocation()
        {
            // Arrange           
            mockFileReader
                .Setup(fr => fr.ReadAllText(It.IsAny<string>()))               
                .Returns(ValidJson);

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
            mockFileReader
                .Setup(fr => fr.ReadAllText(It.IsAny<string>()))
                .Returns(InvalidJson);

            // Act
            Action act = () => trackingService.Get();

            // Assert
            act.Should().Throw<ApplicationException>();

        }

        [Test]
        public void Get_EmptyJson_ShouldThrowApplicationException()
        {
            // Arrange
            mockFileReader
                .Setup(fr => fr.ReadAllText(It.IsAny<string>()))
                .Returns(string.Empty);

            // Act
            Action act = () => trackingService.Get();

            // Assert
            act.Should().Throw<ApplicationException>();
        }

        [TearDown]
        public void TearDown()
        {

        }

    }
}
