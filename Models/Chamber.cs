using System;
using System.Collections.Generic;

#nullable disable

namespace Test
{
    public partial class Chamber
    {
        public int Id { get; set; }
        public int ChamberNumber { get; set; }
        public int ChamberWoodId { get; set; }
        public int ChamberHoursLeft { get; set; }
        public int ChamberHoursSpend { get; set; }
        public int PlanDryingId { get; set; }

        public virtual ChamberWood ChamberWood { get; set; }
        public virtual PlanDrying PlanDrying { get; set; }
    }
}
