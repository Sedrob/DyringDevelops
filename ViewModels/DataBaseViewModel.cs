using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Models
{
    public class DataBaseViewModel 
    {
        public double S1 { get; set; }
        public double S2 { get; set; }

        public int StartDamp { get; set; }
        public int EndDamp { get; set; }

        public double MoveAir { get; set; }
        public string domainCirculation { get; set; }
        public double numericStacks { get; set; }
        public int numericStacksIndex { get; set; }

        public int Tc1 { get; set; }
        public int Tc2 { get; set; }
        public int Tc3 { get; set; }

        public int DtC1 { get; set; }
        public int DtC2 { get; set; }
        public int DtC3 { get; set; }

        //index
        public int CamerValue { get; set; }
        public int LengValue { get; set; }
        public string WidthValue { get; set; }
        public string HeightValue { get; set; }
        public string HeatCarrier { get; set; }
        public int TemperatureCarrier { get; set; }
        //
        public string TreeSpecies { get; set; }
        public int CameraId { get; set; }

        //Данные для получения число камер редактирования
        public int CameraValueId { get; set; }
        //Данные о плане 
        public int? PlanID { get; set; }

    }
}
