using System;
using System.Collections.Generic;

#nullable disable

namespace Test
{
    public partial class SoftDm32
    {
        public SoftDm32()
        {
            SoftDms = new HashSet<SoftDm>();
        }

        public int SoftDm30Id { get; set; }
        public int? TempDry { get; set; }
        public decimal? Humidity { get; set; }
        public int? DurationSteps { get; set; }

        public virtual ICollection<SoftDm> SoftDms { get; set; }
    }
}
