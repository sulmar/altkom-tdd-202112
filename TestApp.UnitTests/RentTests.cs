using NUnit.Framework;
using System;

namespace TestApp.UnitTests
{
    public class RentTests
    {

        // Method_Scenario_ExpectedBehavior
        
        [Test]        
        public void CanReturn_UserIsEmpty_ShouldThrowsArgumentNullException()
        {
            // Arrange
            User rentee = new User();
            Rent rent = new Rent(rentee);

            // Act
            TestDelegate act = () => rent.CanReturn(null);

            // Assert
            Assert.Throws<ArgumentNullException>(act);            
        }

        [Test]
        public void CanReturn_UserIsAdmin_ShouldReturnsTrue()
        {
            // Arrange
            User rentee = new User();
            Rent rent = new Rent(rentee);

            // Act
            bool result = rent.CanReturn(new User { IsAdmin = true });

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void CanReturn_UserIsAdminAndRentee_ShouldReturnsTrue()
        {
            // Arrange
            User rentee = new User();
            Rent rent = new Rent(rentee);

            // Act
            rentee.IsAdmin = true;
            bool result = rent.CanReturn(rentee);

            // Assert
            Assert.IsTrue(result);
        }


        [Test]
        public void CanReturn_UserIsNotAdminAndNotRentee_ShouldReturnsFalse()
        {
            // Arrange
            User rentee = new User();
            Rent rent = new Rent(rentee);

            // Act
            bool result = rent.CanReturn(new User { IsAdmin = false });

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void CanReturn_UserIsNotAdminAndRentee_ShouldReturnsTrue()
        {
            // Arrange
            User rentee = new User();
            Rent rent = new Rent(rentee);

            // Act
            rentee.IsAdmin = false;
            bool result = rent.CanReturn(rentee);

            // Assert
            Assert.IsTrue(result);
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
            Assert.IsNotNull(result);
            Assert.AreEqual(rentee, result.Rentee);
        }

        [Test]
        public void Rent_RenteeIsEmpty_ShouldThrowsArgumentNullException()
        {
            // Arrange

            // Act
            TestDelegate act = () => new Rent(null);

            // Assert
            Assert.Throws<ArgumentNullException>(act);
        }
    }
}