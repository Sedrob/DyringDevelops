using System;
using System.Collections.Generic;

#nullable disable

namespace Test
{
    public partial class SoftDrl25
    {
        public SoftDrl25()
        {
            SoftDrls = new HashSet<SoftDrl>();
        }

        public int SoftDrL25Id { get; set; }
        public int? TempDry { get; set; }
        public decimal? Humidity { get; set; }
        public int? DurationSteps { get; set; }

        public virtual ICollection<SoftDrl> SoftDrls { get; set; }
    }
}
