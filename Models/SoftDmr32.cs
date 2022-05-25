using System;
using System.Collections.Generic;

#nullable disable

namespace Test
{
    public partial class SoftDmr32
    {
        public SoftDmr32()
        {
            SoftDmrs = new HashSet<SoftDmr>();
        }

        public int SoftDm32Id { get; set; }
        public int? TempDry { get; set; }
        public decimal? Humidity { get; set; }
        public int? DurationSteps { get; set; }

        public virtual ICollection<SoftDmr> SoftDmrs { get; set; }
    }
}
