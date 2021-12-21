using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Mocking;

namespace TestApp.UnitTests
{
    public class SalaryCalculatorServiceTests
    {
        [Test]
        public async Task CalculateAsync_AmountPLN_ShouldReturnsTheSameAmount()
        {
            // Arrange
            Mock<IRateService> mockRateService = new Mock<IRateService>();

            mockRateService
                .Setup(rs => rs.GetAsync("PLN"))
                .Returns(Task.FromResult(new Rate { mid = 1 }));

            IRateService rateService = mockRateService.Object;

            SalaryCalculatorService salaryCalculatorService = new SalaryCalculatorService(rateService);

            // Act
            decimal result = await salaryCalculatorService.CalculateAsync(100, "PLN");

            // Assert
            Assert.AreEqual(100, result);
        }

        [Test]
        public async Task CalculateAsync_AmountEUR_ShouldReturnsAmountRatioEUR()
        {
            // Arrange
            Mock<IRateService> mockRateService = new Mock<IRateService>(MockBehavior.Strict);

            mockRateService
                .Setup(rs => rs.GetAsync("EUR"))
                .Returns(Task.FromResult(new Rate { mid = 4 }));

            IRateService rateService = mockRateService.Object;

            SalaryCalculatorService salaryCalculatorService = new SalaryCalculatorService(rateService);

            // Act
            decimal result = await salaryCalculatorService.CalculateAsync(100, "EUR");

            // Assert
            Assert.AreEqual(400, result);
        }
    }
}
