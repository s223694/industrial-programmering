using inventory_assistant.Models;
using System.Collections.Generic;
using System.Linq;


namespace inventory_assistant.Models
{
    public class Inventory
    {
        // Making this dictionary private so it can only be accessed through controlled methods
        private readonly Dictionary<Item, int> stock = new Dictionary<Item, int>();

        // Method to add an item and its quantity
        public void AddItem(Item item, int amount)
        {
            if (stock.ContainsKey(item))
                stock[item] += amount;
            else
                stock[item] = amount;
        }

        // Method to get the quantity of a specific item
        public int GetQuantity(Item item)
        {
            return stock.ContainsKey(item) ? stock[item] : 0;
        }
         public bool CanFulfill(IEnumerable<OrderLine> orderLines)
        {
            foreach (var line in orderLines)
            {
                if (!stock.ContainsKey(line.Item))
                    return false;

                if (stock[line.Item] < line.Quantity)
                    return false;
            }

            return true;
        }

        // Simple version of Consume: just subtract
        public void Consume(IEnumerable<OrderLine> orderLines)
        {
            foreach (var line in orderLines)
            {
                if (stock.ContainsKey(line.Item))
                {
                    stock[line.Item] -= (int)line.Quantity;
                }
            }
        }     

        
        

        // Method to find all low-stock items (amount < 5)
        public List<Item> LowStockItems()
        {
            return stock
                .Where(pair => pair.Value < 5)
                .Select(pair => pair.Key)
                .ToList();
        }
     
    }
}