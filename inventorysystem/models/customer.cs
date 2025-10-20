using System.Collections.Generic;

namespace inventory_assistant.Models
{
    public class Customer
    {
        public string Name { get; }
        public List<Order> Orders { get; } = new();

        public Customer(string name)
        {
            Name = name;
        }

        public void CreateOrder(OrderBook orderBook, Order order)
        {
            Orders.Add(order);
            orderBook.QueueOrder(order);
        }

        public override string ToString() => Name;
    }
}
