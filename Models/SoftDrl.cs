using System;
using System.Collections.Generic;

#nullable disable

namespace Test
{
    public partial class SoftDrl
    {
        public int Id { get; set; }
        public int? Number { get; set; }
        public int? HumidityH { get; set; }
        public int? HumidityK { get; set; }
        public int? SoftDrL22Id { get; set; }
        public int? SoftDrL25Id { get; set; }
        public int? SoftDrL32Id { get; set; }
        public int? SoftDrL40Id { get; set; }
        public int? SoftDrL50Id { get; set; }

        public virtual SoftDrl22 SoftDrL22 { get; set; }
        public virtual SoftDrl25 SoftDrL25 { get; set; }
        public virtual SoftDrl32 SoftDrL32 { get; set; }
        public virtual SoftDrl40 SoftDrL40 { get; set; }
        public virtual SoftDrl50 SoftDrL50 { get; set; }
    }
}
