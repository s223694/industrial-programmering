using System;
using inventory_assistant.Models;


namespace inventory_assistant.Models
{
    public class BulkItem : Item
    {
        public string MeasurementUnit { get; }

        public BulkItem(string name, decimal pricePerUnit, string measurementUnit)
            : base(name, pricePerUnit)
        {
            MeasurementUnit = measurementUnit;
        }

        public override string ToString()
        {
            return $"{Name}: {PricePerUnit} per {MeasurementUnit}";
        }
    }
}
