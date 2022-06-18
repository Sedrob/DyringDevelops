using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Models;
using Test.Formulas;
using Microsoft.EntityFrameworkCore;

namespace Test.Formulas
{
    public class Calculation
    {
        //Заносение Влажности Древесины Whi / Wki
        public List<int> WoodMoistureH(DataBaseViewModel dataBase)
        {
            List<int> wHi = new List<int>();
            wHi.Add(dataBase.StartDamp);
            wHi.Add(35);
            wHi.Add(25);

            return wHi;
        }
        public List<int> WoodMoistureK(DataBaseViewModel dataBase)
        {
            List<int> wKi = new List<int>();

            wKi.Add(35);
            wKi.Add(25);
            wKi.Add(dataBase.EndDamp);

            return wKi;
        }
        //Используется для заносение ф
        double ComputeF(int DtC, int Tc)
        {
            double a = 0.613697 + 0.0435041 * DtC + 0.00149468 * Math.Pow(DtC, 2) + 0.00002501 * Math.Pow(DtC, 3)
              + 0.000000294575 * Math.Pow(DtC, 4) + 0.00000000269553 * Math.Pow(DtC, 5);
            double b = 0.613697 + 0.0435041 * Tc + 0.00149468 * Math.Pow(Tc, 2) + 0.00002501 * Math.Pow(Tc, 3)
              + 0.000000294575 * Math.Pow(Tc, 4) + 0.00000000269553 * Math.Pow(Tc, 5);
            double c = 0.000695 * 101.325 * (Tc - DtC);

            return ((a / b - c / b) * 100);
        }
        //Заносение tc/tm ф
        public List<int> EnvironmentParameters(DataBaseViewModel dataBase, ValuesCalculation values, int x)
        {
            List<int> param = new List<int>();
            if (x == 1)
            {
                param.Add(dataBase.Tc1);
                param.Add(dataBase.Tc2);
                param.Add(dataBase.Tc3);
                return param;
            }
            else if (x == 2)
            {
                param.Add(values.tC[0] - dataBase.DtC1);
                param.Add(values.tC[1] - dataBase.DtC2);
                param.Add(values.tC[2] - dataBase.DtC3);
                return param;
            }
            return param;
        }
        //Заносение ф
        public List<double> EnvironmentF(DataBaseViewModel dataBase, ValuesCalculation values)
        {
            List<double> param = new List<double>();
            param.Add(Math.Round(ComputeF(values.tcM[0], dataBase.Tc1), MidpointRounding.AwayFromZero));
            param.Add(Math.Round(ComputeF(values.tcM[1], dataBase.Tc2), MidpointRounding.AwayFromZero));
            param.Add(Math.Round(ComputeF(values.tcM[2], dataBase.Tc3), MidpointRounding.AwayFromZero));
            return param;
        }
        //Wp % заносение данных 
        public List<double> PercentageHumidity(ValuesCalculation values)
        {
            List<double> param = new List<double>();

            for (int i = 0; i < 3; i++)
            {
                double fi = values.f[i];
                double tc = values.tC[i];
                double a;
                if (fi > 50)
                {
                    a = Math.Round(0.512 / (1.21 - (fi / 100)) * (21.7 - Math.Pow(((273 + tc) / 100), 2)), 1);
                }
                else
                {
                    a = Math.Round(0.36 * (13.9 - Math.Pow((273 + tc) / 100, 2)) + 0.72 * fi / 100 * (29.5 - Math.Pow((273 + tc) / 100, 2)), 1);
                }
                param.Add(a);
            }
            return param;
        }
        //Заполнение Ei
        public List<double> dryingEi(ValuesCalculation values)
        {
            List<double> param = new List<double>();

            for (int i = 0; i < 3; i++)
            {
                param.Add(Math.Round((values.wKi[i] - values.wp[i]) / (values.wHi[i] - values.wp[i]), 3));
            }
            return param;
        }
        // Заполнение Ац и S1/S2
        public void Reversibility(DataBaseViewModel dataBase, ValuesCalculation values)
        {
            if (dataBase.domainCirculation == "Реверсивная")
                values.domainCirculation = 1;
            else
                values.domainCirculation = 1.1;
            
            values.s1s2 = Math.Round(dataBase.S1 / dataBase.S2, 3);
        }
        // Заполнение Aci
        public List<double> SectionAci(ValuesCalculation values)
        {
            List<double> param = new List<double>();

            for (int i = 0; i < 3; i++)
            {
                double result = 0;
                string tm = values.tcM[i].ToString();
                if (tm[0].ToString() == "4")
                {
                    if (Convert.ToInt32(tm) % 10 < 3)
                        result = 1.7;
                    else if (Convert.ToInt32(tm) % 10 > 7)
                        result = 2.2;
                    else
                        result = 2.6;
                }
                else if (tm[0].ToString() == "5")
                {
                    if (Convert.ToInt32(tm) % 10 < 3)
                        result = 2.6;
                    else if (Convert.ToInt32(tm) % 10 > 7)
                        result = 3.1;
                    else
                        result = 3.7;
                }
                else if (tm[0].ToString() == "6")
                {
                    if (Convert.ToInt32(tm) % 10 < 3)
                        result = 3.7;
                    else if (Convert.ToInt32(tm) % 10 > 7)
                        result = 4.3;
                    else
                        result = 5.0;
                }
                else
                    result = 5.0;
                param.Add(result);
            }
            return param;
        }

        //Расчет последних 
        public List<double> ct(DataBaseViewModel dataBase, ValuesCalculation values, ValuesTableCCalculation tableC)
        {
            List<double> param = new List<double>();

            //Старые данные dataSetGreedValues();
            List<double> abc = new List<double>(); 
            List<double> abc1 = new List<double>();
            double min1, min2, min3, max1, max2, max3 = 0;

            string s1s2 = values.s1s2.ToString();
            double s1s2int = Math.Round(values.s1s2, 1);
            if (s1s2.Length == 3)
            {
                abc = tableC.GreedValues(s1s2int);

                param.Add(Math.Round(abc[0] * (0.35 * 0.35) + abc[1] * 0.35 + abc[2], 4));
                param.Add(Math.Round(abc[0] * (0.4865 * 0.4865) + abc[1] * 0.4865 + abc[2], 4));
                param.Add(Math.Round(abc[0] * (0.2584 * 0.2584) + abc[1] * 0.2584 + abc[2], 4));
            }
            else if (s1s2.Length > 3)
            {
                if (Convert.ToInt32(s1s2[3]) < 5)
                {
                    abc = tableC.GreedValues(s1s2int);
                    abc1 = tableC.GreedValues(s1s2int + 0.1);

                    min1 = abc[0] * (0.35 * 0.35) + abc[1] * 0.35 + abc[2];
                    min2 = abc[0] * (0.4865 * 0.4865) + abc[1] * 0.4865 + abc[2];
                    min3 = abc[0] * (0.2584 * 0.2584) + abc[1] * 0.2584 + abc[2];

                    max1 = abc1[0] * (0.35 * 0.35) + abc1[1] * 0.35 + abc1[2];
                    max2 = abc1[0] * (0.4865 * 0.4865) + abc1[1] * 0.4865 + abc1[2];
                    max3 = abc1[0] * (0.2584 * 0.2584) + abc1[1] * 0.2584 + abc1[2];

                    param.Add(Math.Round((max1 - min1) / (0.1) * (0.0) + min1, 4));
                    param.Add(Math.Round((max2 - min2) / (0.1) * (0.0) + min2, 4));
                    param.Add(Math.Round((max3 - min3) / (0.1) * (0.0) + min3, 4));
                }
                else
                {
                    double a = s1s2int - 0.1;
                    abc = tableC.GreedValues(Math.Round(s1s2int - 0.1,1));
                    abc1 = tableC.GreedValues(s1s2int);

                    min1 = Math.Round(abc[0] * (0.35 * 0.35) + abc[1] * 0.35 + abc[2], 4);
                    min2 = Math.Round(abc[0] * (0.4865 * 0.4865) + abc[1] * 0.4865 + abc[2], 4);
                    min3 = Math.Round(abc[0] * (0.2584 * 0.2584) + abc[1] * 0.2584 + abc[2], 4);

                    max1 = Math.Round(abc1[0] * (0.35 * 0.35) + abc1[1] * 0.35 + abc1[2], 4);
                    max2 = Math.Round(abc1[0] * (0.4865 * 0.4865) + abc1[1] * 0.4865 + abc1[2], 4);
                    max3 = Math.Round(abc1[0] * (0.2584 * 0.2584) + abc1[1] * 0.2584 + abc1[2], 4);

                    param.Add(Math.Round((max1 - min1) / (0.1) * (0.1) + min1, 4));
                    param.Add(Math.Round((max2 - min2) / (0.1) * (0.1) + min2, 4));
                    param.Add(Math.Round((max3 - min3) / (0.1) * (0.1) + min3, 4));
                    
                }
            }
            return param;
        }
        public List<double> ctB(DataBaseViewModel dataBase, ValuesCalculation values)
        {
            List<double> param = new List<double>();

            param.Add(Math.Round(values.cT[0] * 
                (65 * Math.Pow(dataBase.S1 / 10, 2) / values.aCi[0] * 1.31), 1));
            param.Add(Math.Round(values.cT[1] *
                ((65 * Math.Pow(dataBase.S1 / 10, 2) / values.aCi[0] * 1.31)), 1));
            param.Add(Math.Round(values.cT[2] *
                ((65 * Math.Pow(dataBase.S1 / 10, 2) / values.aCi[0] * 1.31)), 1));

            return param;
        }
        public List<double> c3(DataBaseViewModel dataBase, ValuesCalculation values, ValuesTableCCalculation tableC)
        {
            List<double> param = new List<double>();

            //dataSetGreedValuesc3();
            List<double> abc = new List<double>();
            List<double> abc1 = new List<double>();
            double min1, min2, min3, max1, max2, max3 = 0;
            if (dataBase.numericStacks == 3)
            {
                abc = tableC.GreedValuesC3(dataBase.numericStacks);

                min1 = abc[0] * (1.21 * 1.21) + abc[1] * 1.21 + abc[2];
                min2 = abc[0] * (1.23 * 1.23) + abc[1] * 1.23 + abc[2];
                min3 = abc[0] * (1.42 * 1.42) + abc[1] * 1.42 + abc[2];

                max1 = abc[0] * (1.21 * 1.21) + abc[1] * 1.21 + abc[1];
                max2 = abc[0] * (1.23 * 1.23) + abc[1] * 1.23 + abc[1];
                max3 = abc[0] * (1.42 * 1.42) + abc[1] * 1.42 + abc[1];

                param.Add(Math.Round((max1 - min1) / (0.5) * (0.0) + min1, 3));
                param.Add(Math.Round((max2 - min2) / (0.5) * (0.0) + min2, 3));
                param.Add(Math.Round((max3 - min3) / (0.5) * (0.0) + min3, 3));

            }
            else
            {
                abc = tableC.GreedValuesC3(dataBase.numericStacks);
                abc1 = tableC.GreedValuesC3(dataBase.numericStacks + 0.5);

                min1 = abc[0] * (1.21 * 1.21) + abc[1] * 1.21 + abc[2];
                min2 = abc[0] * (1.23 * 1.23) + abc[1] * 1.23 + abc[2];
                min3 = abc[0] * (1.42 * 1.42) + abc[1] * 1.42 + abc[2];

                max1 = abc1[0] * (1.21 * 1.21) + abc1[1] * 1.21 + abc1[2];
                max2 = abc1[0] * (1.23 * 1.23) + abc1[1] * 1.23 + abc1[2];
                max3 = abc1[0] * (1.42 * 1.42) + abc1[1] * 1.42 + abc1[2];

                param.Add(Math.Round((max1 - min1) / (0.5) * (0.0) + min1, 3));
                param.Add(Math.Round((max2 - min2) / (0.5) * (0.0) + min2, 3));
                param.Add(Math.Round((max3 - min3) / (0.5) * (0.0) + min3, 3));

            }
            return param;
        }
        //Время 
        public List<double> dryngTime(DataBaseViewModel dataBase, ValuesCalculation values)
        {
            List<double> param = new List<double>();

            double time;
            for (int i = 0; i < 3; i++)
            {
                if (1 / values.eI[i] < 1.4)
                {
                    time = values.cT[i] * 65 * Math.Pow(dataBase.S1 / 10, 2) /
                        values.aCi[i] * values.cC[i] *
                        values.domainCirculation * Math.Log10(1 / values.eI[i]);
                }
                else
                {
                    time = values.cT[i] * 65 * Math.Pow(dataBase.S1 / 10, 2) /
                        values.aCi[i] * values.cC[i] *
                        values.domainCirculation * 0.81 * Math.Log10(1 / values.eI[i]);
                }
                param.Add(Math.Round(time));
            }
            return param;
        }
        //Capacity
        public async void chamberCapacity(ValuesCalculation values, DataBaseViewModel dataBase)
        {
            DryingWood_DBContext dBContext = new DryingWood_DBContext();
            PlanDrying plan = await dBContext.PlanDryings.FirstOrDefaultAsync(p => p.Id == dataBase.PlanID);
            if( plan != null)
            {
                double l = plan.LengValue;
                double b = Convert.ToDouble(plan.WidthValue);
                double h = Convert.ToDouble(plan.HeightValue);
                int m = dataBase.Packages;
                double v = 1;
                switch (dataBase.S1)
                {
                    case 22:
                        v = 0.333;
                        break;
                    case 25:
                        v = 0.356;
                        break;
                    case 32:
                        v = 0.399;
                        break;
                    case 40:
                        v = 0.438;
                        break;
                    case 50:
                        v = 0.474;
                        break;
                }
                values.chCapacity = Math.Round(l * b * h * m * v, 2);
            }

        }

    }
}
