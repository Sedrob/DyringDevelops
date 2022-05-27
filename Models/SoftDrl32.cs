﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Test
{
    public partial class SoftDrl32
    {
        public SoftDrl32()
        {
            SoftDrls = new HashSet<SoftDrl>();
        }

        public int SoftDrL32Id { get; set; }
        public int? TempDry { get; set; }
        public decimal? Humidity { get; set; }
        public int? DurationSteps { get; set; }

        public virtual ICollection<SoftDrl> SoftDrls { get; set; }
    }
}