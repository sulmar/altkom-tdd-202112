using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using TestApp;
using TestApp.Mocking;

namespace Tests
{
    public class OrderServiceTests
    {
        private Mock<IOrderSender> sender;
        private DbOrderService orderService;

        [SetUp]
        public void Setup()
        {
           sender = new Mock<IOrderSender>();
           orderService = new DbOrderService(sender.Object);
        }

        [Test]
        public void Update_WhenSentStatus_SendOrder()
        {
            var order = new Order { Status = OrderStatus.Sent };

            orderService.Update(order);
            
            sender.Verify(c => c.Send(order));
                
        }

        [Test]
        public void Update_WhenBoxingStatus_NotSendOrder()
        {
            var order = new Order { Status = OrderStatus.Boxing };

            orderService.Update(order);

            sender.Verify(c => c.Send(order), Times.Never);

        }
    }

    public class TrackingServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void Get_EmptyFile_ThrowApplicationException()
        {
            var fileReader = new Mock<IFileReader>();
            fileReader.Setup(fr => fr.Get(It.IsAny<string>())).Returns(string.Empty);

            var trackingRepository = new Mock<ITrackingRepository>();

            trackingRepository.Setup(tp => tp.Get()).Returns(
                new List<Tracking>
                {
                    new Tracking { Location = new Location(53.125, 18.011111) }
                });

            var service = new TrackingService(fileReader.Object, trackingRepository.Object);

            var result = service.Get("tracking.txt");

            Assert.That(result, Is.Null);
        }


        [Test]
        public void Get_ValidFile_ReturnLocation()
        {
            var fileReader = new Mock<IFileReader>();

            fileReader.Setup(fr => fr.Get(It.IsAny<string>()))
                .Returns("{\"latitude\":52.01,\"longitude\":28.01}");


            var trackingService = new TrackingService(fileReader.Object, null);

            var location = trackingService.Get("tracking.txt");

            Assert.That(location.Latitude, Is.EqualTo(52.01));
            Assert.That(location.Longitude, Is.EqualTo(28.01));



        }

        [Test]
        public void Get_ValidLocations_ReturnAsGeoHashPath()
        {
            var trackingRepository = new Mock<ITrackingRepository>();

            trackingRepository.Setup(tp => tp.Get())
                .Returns(
               new List<Tracking>
               {
                    new Tracking { Location = new Location(53.125, 18.011111) },
                    new Tracking { Location = new Location(53.125, 18.011111) },
                    new Tracking { Location = new Location(53.125, 18.011111) }
               });

            ITrackingService trackingService = new TrackingService(null, trackingRepository.Object);

            var result = trackingService.GetPathAsGeoHash();

            Assert.That(result, Is.EqualTo("u3ky1z5793cb,u3ky1z5793cb,u3ky1z5793cb"));
        }
    }
}