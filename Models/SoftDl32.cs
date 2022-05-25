using System;
using System.Collections.Generic;

#nullable disable

namespace Test
{
    public partial class SoftDl32
    {
        public SoftDl32()
        {
            SoftDls = new HashSet<SoftDl>();
        }

        public int SoftDl32Id { get; set; }
        public int? TempDry { get; set; }
        public decimal? Humidity { get; set; }
        public int? DurationSteps { get; set; }

        public virtual ICollection<SoftDl> SoftDls { get; set; }
    }
}
