using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Refactoring;

namespace TestApp.UnitTests
{
    public class OrderCalculatorTests
    {
        private OrderCalculator orderCalculator;
        private Order order;
        private Customer customer;

        private readonly TimeSpan StartHappyHour = TimeSpan.Parse("09:00");
        private readonly TimeSpan EndHappyHour = TimeSpan.Parse("16:00");

        private TimeSpan BeforeHappyHour => StartHappyHour.Add(TimeSpan.FromMinutes(-1));
        private TimeSpan AfterHappyHour => EndHappyHour.Add(TimeSpan.FromMinutes(1));

        private DateTime BeforeHappyHourDate => DateTime.MinValue.Add(BeforeHappyHour);
        private DateTime AfterHappyHourDate => DateTime.MinValue.Add(AfterHappyHour);

        private const decimal RegularPrice = 100;

        private const decimal NoDiscount = decimal.Zero;


        [SetUp]
        public void Setup()
        {
            // Arrange
            orderCalculator = new OrderCalculator();

            customer = new Customer(string.Empty, string.Empty);

            order = new Order(DateTime.MinValue, customer);
            order.AddDetail(new Product(1, "a", RegularPrice));

        }

        // Promocja Happy Hours - 10% upustu w godzinach od 9:00 do 15:00
        [Test]
        public void CalculateDiscount_OrderDateBeforeHappyHour_ShouldReturnsNoDiscount()
        {
            // Act          
            order.OrderDate = BeforeHappyHourDate;

            decimal result = orderCalculator.CalculateDiscount(order);

            // Assert
            result.Should().Be(NoDiscount);
            
        }

        [Test]
        public void CalculateDiscount_OrderDateAfterHappyHour_ShouldReturnsNoDiscount()
        {
            // Act          
            order.OrderDate = AfterHappyHourDate;

            decimal result = orderCalculator.CalculateDiscount(order);

            // Assert
            result.Should().Be(NoDiscount);
        }

        [TestCase("09:00")]
        [TestCase("12:00")]
        [TestCase("15:00")]
        public void CalculateDiscount_OrderDateAtHappyHour_ShouldReturnsDiscountedPrice(TimeSpan time)
        {
            // Act          
            order.OrderDate = DateTime.MinValue.Add(time);

            decimal result = orderCalculator.CalculateDiscount(order);

            // Assert
            result.Should().NotBe(NoDiscount);
        }





    }
}
