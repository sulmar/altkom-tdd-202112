using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Fundamentals.Gus;

namespace TestApp.UnitTests
{
    public class ReportFactoryTests
    {
        private const string UnknownType = "X";

        [TestCase("P")]
        [TestCase("LP")]
        [TestCase("LF")]
        public void Create_Type_ShouldReturnsLegalPersonalityReport(string type)
        {
            // Arrange

            // Act
            Report result = ReportFactory.Create(type);

            // Assert
            Assert.That(result, Is.TypeOf<LegalPersonalityReport>());

            result.Should().BeOfType<LegalPersonalityReport>();
        }

        [Test]
        public void Create_TypeF_ShouldReturnsSoleTraderReport()
        {
            // Arrange

            // Act
            Report result = ReportFactory.Create("F");

            // Assert
            Assert.That(result, Is.TypeOf<SoleTraderReport>());

            result.Should().BeOfType<SoleTraderReport>();
        }

        [Test]
        public void Create_UnknownType_ShouldThrowsNotSupportedException()
        {
            // Arrange

            // Act
            Action act = () => ReportFactory.Create(UnknownType);

            // Assert
            act.Should().Throw<NotSupportedException>();
        }


    }
}
