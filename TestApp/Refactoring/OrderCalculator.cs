using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace TestApp.Refactoring
{
    #region Models

    public class Order
    {
        public DateTime OrderDate { get; set; }
        public Customer Customer { get; set; }
        public decimal Amount => Details.Sum(p => p.LineTotal);

        public ICollection<OrderDetail> Details = new Collection<OrderDetail>();

        public void AddDetail(Product product, int quantity = 1)
        {
            OrderDetail detail = new OrderDetail(product, quantity);

            this.Details.Add(detail);
        }

        public Order(DateTime orderDate, Customer customer)
        {
            OrderDate = orderDate;
            Customer = customer;
        }
    }

    public class Product
    {
        public Product(int id, string name, decimal unitPrice)
        {
            Id = id;
            Name = name;
            UnitPrice = unitPrice;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class OrderDetail
    {
        public OrderDetail(Product product, int quantity = 1)
        {
            Product = product;
            Quantity = quantity;

            UnitPrice = product.UnitPrice;
        }

        public int Id { get; set; }
        public Product Product { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal LineTotal => UnitPrice * Quantity;
    }

    public class Customer
    {
        public Customer(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            if (firstName.EndsWith("a"))
            {
                Gender = Gender.Female;
            }
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }

    }

    public enum Gender
    {
        Male,
        Female
    }

    #endregion

    // Strategy
    public interface ICanDiscountStrategy
    {
        bool CanDiscount(Order order);
    }

    public interface IDiscountStrategy
    {
        decimal CalculateDiscount(Order order);
    }

    public class HappyHoursDiscountStrategy : ICanDiscountStrategy
    {
        private readonly TimeSpan from;
        private readonly TimeSpan to;

        public HappyHoursDiscountStrategy(TimeSpan from, TimeSpan to)
        {
            this.from = from;
            this.to = to;
        }

        public bool CanDiscount(Order order) => order.OrderDate.TimeOfDay >= from && order.OrderDate.TimeOfDay <= to;
    }

    public class PercentageDiscountStrategy : IDiscountStrategy
    {
        private readonly decimal percentage;

        public PercentageDiscountStrategy(decimal percentage) => this.percentage = percentage;

        public decimal CalculateDiscount(Order order) => order.Amount * percentage;
    }

    public class FixedDiscountStrategy : IDiscountStrategy
    {
        private readonly decimal fixedDiscount;

        public FixedDiscountStrategy(decimal fixedDiscount)
        {
            this.fixedDiscount = fixedDiscount;
        }

        public decimal CalculateDiscount(Order order)
        {
            if (order.Amount < fixedDiscount)
            {
                return order.Amount;
            }
            else
                return fixedDiscount;
        }
    }

    public class OrderCalculator
    {
        private readonly ICanDiscountStrategy canDiscountStrategy;
        private readonly IDiscountStrategy discountStrategy;

        public OrderCalculator(ICanDiscountStrategy canDiscountStrategy, IDiscountStrategy discountStrategy)
        {
            this.canDiscountStrategy = canDiscountStrategy;
            this.discountStrategy = discountStrategy;
        }

        public decimal CalculateDiscount(Order order)
        {
            if (canDiscountStrategy.CanDiscount(order)) // Predykat
            {
                return discountStrategy.CalculateDiscount(order); 
            }
            else

                return decimal.Zero;
        }
    }

    public class HappyHourOrderCalculator
    {
        private readonly TimeSpan from;
        private readonly TimeSpan to;
        private readonly decimal percentage;

        public HappyHourOrderCalculator(TimeSpan from, TimeSpan to, decimal percentage)
        {
            this.from = from;
            this.to = to;
            this.percentage = percentage;
        }

        public decimal CalculateDiscount(Order order)
        {          
            if (order.OrderDate.TimeOfDay >= from && order.OrderDate.TimeOfDay <= to)
            {
                return order.Amount * percentage;
            }
            else

                return decimal.Zero;
        }
    }

    public class GenderOrderCalculator
    {
        private readonly Gender gender;
        private readonly decimal percentage;

        public GenderOrderCalculator(Gender gender, decimal percentage)
        {
            this.gender = gender;
            this.percentage = percentage;
        }

        public decimal CalculateDiscount(Order order)
        {
            if (order.Customer.Gender == gender)
            {
                return order.Amount * percentage;
            }
            else

                return decimal.Zero;
        }
    }


  


    // Promocja 20% upustu dla kobiet

    // Powyżej powyżej 100 PLN - upust 10 zł, 200 PLN upust 20 PLN

    // Promocja weekendowa - wysyłka gratis



}
