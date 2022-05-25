using System;
using System.Collections.Generic;

#nullable disable

namespace Test
{
    public partial class ChamberWood
    {
        public ChamberWood()
        {
            Chambers = new HashSet<Chamber>();
        }

        public int Id { get; set; }
        public string TypeWood { get; set; }
        public int TimeHours { get; set; }
        public int Position { get; set; }
        public int ValueId { get; set; }

        public virtual ValueWood Value { get; set; }
        public virtual ICollection<Chamber> Chambers { get; set; }
    }
}
