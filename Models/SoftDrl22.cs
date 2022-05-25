using System;
using System.Collections.Generic;

#nullable disable

namespace Test
{
    public partial class SoftDrl22
    {
        public SoftDrl22()
        {
            SoftDrls = new HashSet<SoftDrl>();
        }

        public int SoftDrL22Id { get; set; }
        public int? TempDry { get; set; }
        public decimal? Humidity { get; set; }
        public int? DurationSteps { get; set; }

        public virtual ICollection<SoftDrl> SoftDrls { get; set; }
    }
}
