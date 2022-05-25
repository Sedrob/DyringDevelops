using System;
using System.Collections.Generic;

#nullable disable

namespace Test
{
    public partial class SoftDl
    {
        public int Id { get; set; }
        public int? Number { get; set; }
        public int? HumidityH { get; set; }
        public int? HumidityK { get; set; }
        public int SoftDl22Id { get; set; }
        public int SoftDl25Id { get; set; }
        public int SoftDl32Id { get; set; }
        public int SoftDl40Id { get; set; }
        public int SoftDl50Id { get; set; }

        public virtual SoftDl22 SoftDl22 { get; set; }
        public virtual SoftDl25 SoftDl25 { get; set; }
        public virtual SoftDl32 SoftDl32 { get; set; }
        public virtual SoftDl40 SoftDl40 { get; set; }
        public virtual SoftDl50 SoftDl50 { get; set; }
    }
}
