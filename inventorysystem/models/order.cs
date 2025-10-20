using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;

namespace inventory_assistant.Models
{
    public class Order
    {
        public string CustomerName { get; }
        public DateTime Time { get; }
        public ObservableCollection<OrderLine> OrderLines { get; }

        // ✅ Property, not method
        public decimal TotalPrice => 
            OrderLines.Sum(line => line.Item.PricePerUnit * (decimal)line.Quantity);

        public Order(string customerName, IEnumerable<OrderLine> orderLines)
        {
            CustomerName = customerName;
            Time = DateTime.Now;
            OrderLines = new ObservableCollection<OrderLine>(orderLines);
        }
        public string OrderSummary
{
        get
        {
        return string.Join(", ", OrderLines.Select(line => line.ToString()));
        }
}
        
    }
}
