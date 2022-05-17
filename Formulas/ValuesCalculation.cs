﻿using System;
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

            values.time = values.cTime.ToArray().Sum();
        }
    }
}