using System;
using System.Collections.Generic;

#nullable disable

namespace Test
{
    public partial class SoftDrl50
    {
        public SoftDrl50()
        {
            SoftDrls = new HashSet<SoftDrl>();
        }

        public int SoftDrL50Id { get; set; }
        public int? TempDry { get; set; }
        public decimal? Humidity { get; set; }
        public int? DurationSteps { get; set; }

        public virtual ICollection<SoftDrl> SoftDrls { get; set; }
    }
}
