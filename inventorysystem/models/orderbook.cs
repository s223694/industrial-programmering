using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace inventory_assistant.Models
{
    public class OrderBook 
    {
        public ObservableCollection<Order> QueuedOrders { get; } = new();
        public ObservableCollection<Order> ProcessedOrders { get; } = new();
        

        private readonly Inventory _inventory;

        public OrderBook(Inventory inventory)
        {
            _inventory = inventory;
        }

        public void QueueOrder(Order order)
        {
            QueuedOrders.Add(order);
        }

       public decimal ProcessNextOrder()
{
    if (QueuedOrders.Count == 0)
        return 0;

    var order = QueuedOrders.First();

    if (!_inventory.CanFulfill(order.OrderLines))
        return 0;

    _inventory.Consume(order.OrderLines);

    QueuedOrders.Remove(order);
    ProcessedOrders.Insert(0, order);

    return order.TotalPrice; // ✅ return value instead of storing it
}

    }
}
