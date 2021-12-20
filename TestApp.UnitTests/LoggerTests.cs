using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.UnitTests
{
    public class LoggerTests
    {
        private const string ValidMessage = "a";

        [Test]
        public void Log_ValidMessage_ShouldSetLastMessage()
        {
            // Arrange
            Logger logger = new Logger();

            // Act
            logger.Log(ValidMessage);

            // Assert
            logger.LastMessage.Should().Be(ValidMessage);
        }

        [Test]
        public void Log_ValidMessage_ShouldRaiseMessageLogged()
        {
            // Arrange
            Logger logger = new Logger();

            DateTime logDate = DateTime.MinValue;
            logger.MessageLogged += (sender, args) => logDate = args;

            // Act
            logger.Log(ValidMessage);

            // Assert
            logDate.Should().NotBe(DateTime.MinValue);
        }

        [Test]
        public void Log_ValidMessage_ShouldRaiseMessageLogged2()
        {
            // Arrange
            Logger logger = new Logger();
            using var monitoredLogger = logger.Monitor();            

            // Act
            logger.Log(ValidMessage);

            // Assert
            monitoredLogger.Should().Raise(nameof(Logger.MessageLogged));
        }

        [Test]
        public void Log_ValidMessage_ShouldRaiseExaclyOnceMessageLogged()
        {
            // Arrange
            Logger logger = new Logger();

            int counter = 0;

            logger.MessageLogged += (sender, args) => counter++;

            // Act
            logger.Log(ValidMessage);

            // Assert
            counter.Should().Be(1);
        }


    }
}
