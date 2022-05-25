using System;
using System.Collections.Generic;

#nullable disable

namespace Test
{
    public partial class SoftDm25
    {
        public SoftDm25()
        {
            SoftDms = new HashSet<SoftDm>();
        }

        public int SoftDm25Id { get; set; }
        public int? TempDry { get; set; }
        public decimal? Humidity { get; set; }
        public int? DurationSteps { get; set; }

        public virtual ICollection<SoftDm> SoftDms { get; set; }
    }
}
