using System;
using System.Collections.Generic;

#nullable disable

namespace Test
{
    public partial class SoftDmr25
    {
        public SoftDmr25()
        {
            SoftDmrs = new HashSet<SoftDmr>();
        }

        public int SoftDm25Id { get; set; }
        public int? TempDry { get; set; }
        public decimal? Humidity { get; set; }
        public int? DurationSteps { get; set; }

        public virtual ICollection<SoftDmr> SoftDmrs { get; set; }
    }
}
