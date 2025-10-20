using System;
using inventory_assistant.Models;

namespace inventory_assistant.Models
{
    public class UnitItem : Item
    {
        public decimal Weight { get; }

        public UnitItem(string name, decimal pricePerUnit, decimal weight)
            : base(name, pricePerUnit)
        {
            Weight = weight;
        }

        public override string ToString()
        {
            return $"{Name}: {PricePerUnit} per unit (Weight: {Weight} kg)";
        }
    }
}
