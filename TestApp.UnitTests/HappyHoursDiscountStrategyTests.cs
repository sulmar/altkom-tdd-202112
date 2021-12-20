using FluentAssertions;
using NUnit.Framework;
using System;
using TestApp.Refactoring;

namespace TestApp.UnitTests
{
    public class HappyHoursDiscountStrategyTests
    {
        private readonly TimeSpan StartHappyHour = TimeSpan.Parse("09:00");
        private readonly TimeSpan EndHappyHour = TimeSpan.Parse("16:00");

        private TimeSpan BeforeHappyHour => StartHappyHour.Add(TimeSpan.FromMinutes(-1));
        private TimeSpan AfterHappyHour => EndHappyHour.Add(TimeSpan.FromMinutes(1));
        private DateTime BeforeHappyHourDate => DateTime.MinValue.Add(BeforeHappyHour);
        private DateTime AfterHappyHourDate => DateTime.MinValue.Add(AfterHappyHour);

        private Order order;
        private Customer customer;

        [SetUp]
        public void SetUp()
        {
            customer = new Customer(string.Empty, string.Empty);

            order = new Order(DateTime.MinValue, customer);
        }

        public void CanDiscount_OrderDateBeforeHappyHour_ShouldReturnsFalse()
        {
            // Arrange
            ICanDiscountStrategy canDiscountStrategy = new HappyHoursDiscountStrategy(StartHappyHour, EndHappyHour);

            // Act
            order.OrderDate = BeforeHappyHourDate;
            bool result = canDiscountStrategy.CanDiscount(order);

            // Assert
            result.Should().BeFalse();
        }
    }
}
