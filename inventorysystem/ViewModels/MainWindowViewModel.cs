using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using inventory_assistant.Models;

namespace inventory_assistant.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public OrderBook OrderBook { get; }
        public Inventory Inventory { get; }

        private decimal _totalRevenue;
        public decimal TotalRevenue
        {
            get => _totalRevenue;
            set
            {
                if (_totalRevenue != value)
                {
                    _totalRevenue = value;
                    OnPropertyChanged(); 
                    Console.WriteLine($"[DEBUG] TotalRevenue updated: {_totalRevenue}");
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public MainWindowViewModel()
        {
            Inventory = new Inventory();
            OrderBook = new OrderBook(Inventory);

            var hydralic_pump = new UnitItem("Hydralic Pump", 4.0m, 0.15m);
            var screws = new BulkItem("Screws", 3.50m, "kg");
            var plc_module = new UnitItem("PLC Module", 0.80m, 0.2m);
            var servo_motor = new UnitItem("Servo Motor", 15.0m, 1.5m);


            Inventory.AddItem(hydralic_pump, 9);
            Inventory.AddItem(screws, 50);
            Inventory.AddItem(plc_module, 7);
            Inventory.AddItem(servo_motor, 4);

            var customer1 = new Customer("Alice");
            var customer2 = new Customer("Bob");

            var order1 = new Order(customer1.Name, new[]
            {
                new OrderLine(hydralic_pump, 5),
                new OrderLine(screws, 2)
            });

            var order2 = new Order(customer2.Name, new[]
            {
                new OrderLine(plc_module, 3)
            });

            customer1.CreateOrder(OrderBook, order1);
            customer2.CreateOrder(OrderBook, order2);

            // Just to confirm data loads
            Console.WriteLine($"Queued orders at startup: {OrderBook.QueuedOrders.Count}");
        }

   public void ProcessNextOrder()
{
    var orderRevenue = OrderBook.ProcessNextOrder();
    if (orderRevenue > 0)
    {
        TotalRevenue += orderRevenue;
            Console.WriteLine($"Revenue now: {TotalRevenue}");
           
        
    }
    else
    {
        Console.WriteLine("No orders left or not enough stock.");
    }
}

    }
}
