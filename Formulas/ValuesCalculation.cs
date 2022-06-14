using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Models;
using Test.Formulas;

namespace Test.Formulas
{
    public class ValuesCalculation
    {
        public List<int> wHi { get; set; }

        public List<int> wKi { get; set; }

        public List<int> tC { get; set; }
        public List<int> tcM { get; set; }
        public List<double> f { get; set; }

        public List<double> wp { get; set; }

        public List<double> eI { get; set; }

        public List<double> aCi { get; set; }

        public double domainCirculation { set; get; }

        public double s1s2 { set; get; }

        public List<double> cT { get; set; }

        public List<double> cTB { get; set; }

        public List<double> cC { get; set; }

        public List<double> cTime { get; set; }

        public double time { set; get; }
        public int camerValue { get; set; }
        public Decimal cameraTimeValue { get; set; }
        public int iteration { get; set; }
        public string wood { get; set; }
        public double s1 { get; set; }
        public double s2 { get; set; }

        //Capacity
        public double chCapacity { get; set; }

        public void Values(Calculation calc, DataBaseViewModel dataBase, ValuesCalculation values, ValuesTableCCalculation tableC)
        {
            wHi = calc.WoodMoistureH(dataBase);
            wKi = calc.WoodMoistureK(dataBase);

            tC = calc.EnvironmentParameters(dataBase, values, 1);
            tcM = calc.EnvironmentParameters(dataBase, values, 2);
            f = calc.EnvironmentF(dataBase, values);

            wp = calc.PercentageHumidity(values);
            eI = calc.dryingEi(values);
            aCi = calc.SectionAci(values);

            calc.Reversibility(dataBase, values);

            cT = calc.ct(dataBase, values, tableC);
            cTB = calc.ctB(dataBase, values);
            cC = calc.c3(dataBase, values, tableC);

            cTime = calc.dryngTime(dataBase, values);

            camerValue = dataBase.CamerValue;
            wood = dataBase.TreeSpecies;
            s1 = dataBase.S1;
            s2 = dataBase.S2;
            time = cTime.ToArray().Sum();

            if (wood != "Лиственница")
            {
                if (s1 < 25) time += 8;
                else if (s1 <= 32) time += 10;
                else if (s1 <= 40) time += 12;
                else if (s1 <= 50) time += 14;
            }
            else if(wood != null)
            {
                if (s1 <= 22) time += 8;
                else if (s1 <= 32) time += 10;
                else if (s1 <= 50) time += 12;
            }

            //
            cameraTimeValue = ((int)time + 8) / 24;
            cameraTimeValue = Math.Ceiling(Convert.ToDecimal((int)time + 8)) / 24;
            //

            calc.chamberCapacity(this, dataBase);

            iteration += 1;
        }
    }
}
