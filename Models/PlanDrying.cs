using System;
using System.Collections.Generic;

#nullable disable

namespace Test
{
    public partial class PlanDrying
    {
        public PlanDrying()
        {
            Chambers = new HashSet<Chamber>();
        }

        public int Id { get; set; }
        public int ValueChamber { get; set; }
        public string MonthDrying { get; set; }
        public decimal Utility { get; set; }
        public int HoursLeftDrying { get; set; }
        public int HoursSpendDrying { get; set; }
        public int LengValue { get; set; }
        public string WidthValue { get; set; }
        public string HeightValue { get; set; }
        public string HeatCarrier { get; set; }
        public int TemperatureCarrier { get; set; }

        public virtual ICollection<Chamber> Chambers { get; set; }
    }
}
