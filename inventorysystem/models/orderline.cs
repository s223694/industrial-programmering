using inventory_assistant.Models;

namespace inventory_assistant.Models
{
    public class OrderLine
    {
        public Item Item { get; }
        public decimal Quantity { get; }

        public OrderLine(Item item, decimal quantity) 
        {
            Item = item;
            Quantity = quantity;
        }

        public decimal LineTotal => Item.PricePerUnit * Quantity;

        public override string ToString()
        {
         return $"{Item.Name} x{Quantity}";
        }

    }
    
}
