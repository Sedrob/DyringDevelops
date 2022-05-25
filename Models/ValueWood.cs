using System;
using System.Collections.Generic;

#nullable disable

namespace Test
{
    public partial class ValueWood
    {
        public ValueWood()
        {
            ChamberWoods = new HashSet<ChamberWood>();
        }

        public int Id { get; set; }
        public int StartWetness { get; set; }
        public int EndWetness { get; set; }
        public int StartWidth { get; set; }
        public int EndWidth { get; set; }
        public string TypeWood { get; set; }
        public int HoursWood { get; set; }

        public virtual ICollection<ChamberWood> ChamberWoods { get; set; }
    }
}
