using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Fundamentals;

namespace TestApp.UnitTests
{
    public class InventoryTests
    {
        private Inventory inventory;

        [SetUp]
        public void SetUp()
        {
            inventory = new Inventory();

            inventory.InventoryItems = new List<InventoryItem>
            {
                new InventoryItem { Price = 50, Name = "b" },
                new InventoryItem { Price = 200 ,Name = "a" },
                new InventoryItem { Price = 100 ,Name = "a" },
                new InventoryItem { Price = 200, Name = "c" },
            };
        }


        [Category("A")]
        [Author("Marcin")]
        [Test]
        public void SortByName_OrderAscending_ShouldInventoryItemsSortedAscendingByNameAndAscendingByPrice()
        {
            // Act
            inventory.SortByName(Order.Ascending);

            // Assert
            Assert.That(inventory.InventoryItems,
                Is.Ordered.Ascending.By(nameof(InventoryItem.Name))
                                .Then.Ascending.By(nameof(InventoryItem.Price)));
           
        }

        [Category("A")]
        [Author("Kuba")]
        [Test]
        public void SortByName_OrderDescending_ShouldInventoryItemsSortedDescendingByNameAndAscendingByPrice()
        {
            // Act
            inventory.SortByName(Order.Ascending);

            // Assert
            Assert.That(inventory.InventoryItems,
                Is.Ordered.Descending.By(nameof(InventoryItem.Name))
                                .Then.Ascending.By(nameof(InventoryItem.Price)));

        }

        [Category("B")]
        [Test]
        public void SortByPrice_OrderAscending_ShouldInventoryItemsSortedAscendingByPriceAndAscendingByName()
        {
            // Act
            inventory.SortByPrice(Order.Ascending);

            // Assert
            Assert.That(inventory.InventoryItems,
                Is.Ordered.Ascending.By(nameof(InventoryItem.Price))
                                .Then.Ascending.By(nameof(InventoryItem.Name)));

        }

        [Category("B")]
        [Test]
        public void SortByPrice_OrderDescending_ShouldInventoryItemsSortedDescendingByPriceAndAscendingByName()
        {
            // Act
            inventory.SortByPrice(Order.Descending);

            // Assert
            Assert.That(inventory.InventoryItems,
                Is.Ordered.Descending.By(nameof(InventoryItem.Price))
                                .Then.Ascending.By(nameof(InventoryItem.Name)));
           
        }

    }
}
