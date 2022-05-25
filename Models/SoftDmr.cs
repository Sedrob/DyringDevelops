using System;
using System.Collections.Generic;

#nullable disable

namespace Test
{
    public partial class SoftDmr
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int? HumidityH { get; set; }
        public int? HumidityK { get; set; }
        public int SoftDm22Id { get; set; }
        public int SoftDm25Id { get; set; }
        public int SoftDm32Id { get; set; }
        public int SoftDm40Id { get; set; }
        public int SoftDm50Id { get; set; }

        public virtual SoftDmr22 SoftDm22 { get; set; }
        public virtual SoftDmr25 SoftDm25 { get; set; }
        public virtual SoftDmr32 SoftDm32 { get; set; }
        public virtual SoftDmr40 SoftDm40 { get; set; }
        public virtual SoftDmr50 SoftDm50 { get; set; }
    }
}
