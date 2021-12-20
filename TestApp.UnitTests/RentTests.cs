using FluentAssertions;
using NUnit.Framework;
using System;

namespace TestApp.UnitTests
{
    public class RentTests
    {
        private User rentee;
        private Rent rent;
        
        [SetUp]
        public void Setup()
        {
            // Arrange
            rentee = new User();
            rent = new Rent(rentee);
        }

        // Method_Scenario_ExpectedBehavior
        
        [Test]        
        public void CanReturn_UserIsEmpty_ShouldThrowsArgumentNullException()
        {
            // Act
            TestDelegate act = () => rent.CanReturn(null);

            // Assert
            Assert.Throws<ArgumentNullException>(act);            
        }

        [Test]
        public void CanReturn_UserIsAdmin_ShouldReturnsTrue()
        {
            // Act
            bool result = rent.CanReturn(new User { IsAdmin = true });

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public void CanReturn_UserIsAdminAndRentee_ShouldReturnsTrue()
        {
            // Act
            rentee.IsAdmin = true;
            bool result = rent.CanReturn(rentee);

            // Assert
            Assert.IsTrue(result);
        }


        [Test]
        public void CanReturn_UserIsNotAdminAndNotRentee_ShouldReturnsFalse()
        {
            // Act
            bool result = rent.CanReturn(new User { IsAdmin = false });

            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public void CanReturn_UserIsNotAdminAndRentee_ShouldReturnsTrue()
        {
            // Act
            rentee.IsAdmin = false;
            bool result = rent.CanReturn(rentee);

            // Assert
            result.Should().BeTrue();
        }


        // 1. User jest null -> ArgumentNullException
        // 2. User jest admin -> true
        // (3. User jest adminem i jest wypo¿yczaj¹cym) -> true
        // 4. User nie jest adminem i nie jest wypo¿yczaj¹cym -> 
        // 5. User nie jest adminem i jest wypo¿yczaj¹cym -> true


        [Test]
        public void Rent_RenteeIsNotEmpty_ShouldReturnsRentWithRentee()
        {
            // Arrange           
            User rentee = new User();

            // Act
            Rent result = new Rent(rentee);

            // Assert
            //Assert.IsNotNull(result);
            //Assert.AreEqual(rentee, result.Rentee);

            result.Should().NotBeNull();
            result.Rentee.Should().BeSameAs(rentee);
        }

        [Test]
        public void Rent_RenteeIsEmpty_ShouldThrowsArgumentNullException()
        {
            // Arrange

            // Act
            Action act = () => new Rent(null);

            // Assert
            // Assert.Throws<ArgumentNullException>(act);

            act.Should().Throw<ArgumentNullException>();


        }
    }
}