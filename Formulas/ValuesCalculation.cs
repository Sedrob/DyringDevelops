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
        public IEnumerable<Chamber> chambers { get; set; }

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
            
            time = cTime.ToArray().Sum();
            camerValue = dataBase.CamerValue;

            cameraTimeValue = ((int)time + 8) / 24;
            cameraTimeValue = Math.Ceiling(Convert.ToDecimal((int)time + 8)) / 24;


            iteration += 1;
        }
    }
}
