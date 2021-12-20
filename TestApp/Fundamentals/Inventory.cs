using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Fundamentals
{
    public class Inventory
    {
        public IEnumerable<InventoryItem> InventoryItems { get; set; }

        public void SortByPrice(Order order)
        {
            if (order == Order.Ascending)
                InventoryItems = InventoryItems.OrderBy(item => item.Price);
            else
                InventoryItems = InventoryItems.OrderByDescending(item => item.Name);
        }

        public void SortByName(Order order)
        {
            if (order == Order.Descending)
                InventoryItems = InventoryItems.OrderBy(item => item.Name);
            else
                InventoryItems = InventoryItems.OrderByDescending(item => item.Name);
        }
    }

    public class InventoryItem
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public enum Order
    {
        Ascending,
        Descending
    }
}
