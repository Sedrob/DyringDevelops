using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Models;
using Test.Formulas;

namespace Test.Formulas
{
    public class Calculation
    {
        //Заносение Влажности Древесины Whi / Wki
        void WoodMoisture(DataBaseViewModel dataBase, ValuesCalculation values)
        {
            values.wHi.Add(dataBase.StartDamp);
            values.wHi.Add(35);
            values.wHi.Add(25);


            values.wKi.Add(35);
            values.wKi.Add(25);
            values.wKi.Add(dataBase.StartDamp);

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
        void EnvironmentParameters(DataBaseViewModel dataBase, ValuesCalculation values)
        {
            values.tC.Add(dataBase.Tc1);
            values.tC.Add(dataBase.Tc2);
            values.tC.Add(dataBase.Tc3);

            values.tcM.Add(values.tC[0] - dataBase.DtC1);
            values.tcM.Add(values.tC[1] - dataBase.DtC2);
            values.tcM.Add(values.tC[2] - dataBase.DtC3);

            values.f.Add(Math.Round(ComputeF(values.tcM[0], dataBase.Tc1), MidpointRounding.AwayFromZero));
            values.f.Add(Math.Round(ComputeF(values.tcM[1], dataBase.Tc2), MidpointRounding.AwayFromZero));
            values.f.Add(Math.Round(ComputeF(values.tcM[2], dataBase.Tc3), MidpointRounding.AwayFromZero));
        }
        //Wp % заносение данных 
        void PercentageHumidity(ValuesCalculation values)
        {
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
                values.wp[i] = a;
            }
        }
        //Заполнение Ei
        void dryingEi(ValuesCalculation values)
        {
            for (int i = 0; i < 3; i++)
            {
                values.eI.Add(Math.Round((values.wKi[i] - values.wp[i]) / (values.wHi[i] - values.wp[i]), 3));
            }
        }
        // Заполнение Ац и S1/S2
        void Reversibility(DataBaseViewModel dataBase, ValuesCalculation values)
        {
            if (dataBase.domainCirculation == "Реверсивная")
                values.domainCirculation = 1;
            else
                values.domainCirculation = 1.1;
            
            values.s1s2 = Math.Round(dataBase.S1 / dataBase.S2, 3);
        }
        // Заполнение Aci
        void SectionAci(ValuesCalculation values)
        {
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
                values.aCi.Add(result);
            }

        }

        //Расчет последних 
        void ct(DataBaseViewModel dataBase, ValuesCalculation values, ValuesTableCCalculation tableC)
        {
            //Старые данные dataSetGreedValues();
            List<double> abc = new List<double>(); 
            List<double> abc1 = new List<double>();
            double min1, min2, min3, max1, max2, max3 = 0;

            string s1s2 = values.s1s2.ToString();
            double s1s2int = Math.Round(values.s1s2, 1);
            if (s1s2.Length == 3)
            {
                abc = tableC.GreedValues(s1s2int);

                values.cT.Add(Math.Round(abc[0] * (0.35 * 0.35) + abc[1] * 0.35 + abc[2], 4));
                values.cT.Add(Math.Round(abc[0] * (0.4865 * 0.4865) + abc[1] * 0.4865 + abc[2], 4));
                values.cT.Add(Math.Round(abc[0] * (0.2584 * 0.2584) + abc[1] * 0.2584 + abc[2], 4));
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

                    values.cT.Add(Math.Round((max1 - min1) / (0.1) * (0.0) + min1, 4));
                    values.cT.Add(Math.Round((max2 - min2) / (0.1) * (0.0) + min2, 4));
                    values.cT.Add(Math.Round((max3 - min3) / (0.1) * (0.0) + min3, 4));
                }
                else
                {
                    abc = tableC.GreedValues(s1s2int - 0.1);
                    abc1 = tableC.GreedValues(s1s2int);

                    min1 = Math.Round(abc[0] * (0.35 * 0.35) + abc[1] * 0.35 + abc[2], 4);
                    min2 = Math.Round(abc[0] * (0.4865 * 0.4865) + abc[1] * 0.4865 + abc[2], 4);
                    min3 = Math.Round(abc[0] * (0.2584 * 0.2584) + abc[1] * 0.2584 + abc[2], 4);

                    max1 = Math.Round(abc1[0] * (0.35 * 0.35) + abc1[1] * 0.35 + abc1[2], 4);
                    max2 = Math.Round(abc1[0] * (0.4865 * 0.4865) + abc1[1] * 0.4865 + abc1[2], 4);
                    max3 = Math.Round(abc1[0] * (0.2584 * 0.2584) + abc1[1] * 0.2584 + abc1[2], 4);

                    values.cT.Add(Math.Round((max1 - min1) / (0.1) * (0.1) + min1, 4));
                    values.cT.Add(Math.Round((max2 - min2) / (0.1) * (0.1) + min2, 4));
                    values.cT.Add(Math.Round((max3 - min3) / (0.1) * (0.1) + min3, 4));
                    
                }
            }
        }
        void ctB(DataBaseViewModel dataBase, ValuesCalculation values)
        {
            values.cTB.Add(Math.Round(values.cT[0] * 
                (65 * Math.Pow(dataBase.S1 / 10, 2) / values.aCi[0] * 1.31), 1));
            values.cTB.Add(Math.Round(values.cT[1] *
                ((65 * Math.Pow(dataBase.S1 / 10, 2) / values.aCi[0] * 1.31)), 1));
            values.cTB.Add(Math.Round(values.cT[2] *
                ((65 * Math.Pow(dataBase.S1 / 10, 2) / values.aCi[0] * 1.31)), 1));
        }
        void c3(DataBaseViewModel dataBase, ValuesCalculation values, ValuesTableCCalculation tableC)
        {
            //dataSetGreedValuesc3();
            List<double> abc = new List<double>();
            List<double> abc1 = new List<double>();
            double min1, min2, min3, max1, max2, max3 = 0;
            if (dataBase.numericStacks == 3)
            {
                abc = tableC.GreedValues(3);

                min1 = abc[0] * (1.21 * 1.21) + abc[1] * 1.21 + abc[2];
                min2 = abc[0] * (1.23 * 1.23) + abc[1] * 1.23 + abc[2];
                min3 = abc[0] * (1.42 * 1.42) + abc[1] * 1.42 + abc[2];

                max1 = abc[0] * (1.21 * 1.21) + abc[1] * 1.21 + abc[1];
                max2 = abc[0] * (1.23 * 1.23) + abc[1] * 1.23 + abc[1];
                max3 = abc[0] * (1.42 * 1.42) + abc[1] * 1.42 + abc[1];

                values.cC.Add(Math.Round((max1 - min1) / (0.5) * (0.0) + min1, 3));
                values.cC.Add(Math.Round((max2 - min2) / (0.5) * (0.0) + min2, 3));
                values.cC.Add(Math.Round((max3 - min3) / (0.5) * (0.0) + min3, 3));

            }
            else
            {
                abc = tableC.GreedValues(dataBase.numericStacks);
                abc1 = tableC.GreedValues(dataBase.numericStacks + 0.5);

                min1 = abc[0] * (1.21 * 1.21) + abc[1] * 1.21 + abc[2];
                min2 = abc[0] * (1.23 * 1.23) + abc[1] * 1.23 + abc[2];
                min3 = abc[0] * (1.42 * 1.42) + abc[1] * 1.42 + abc[2];

                max1 = abc1[0] * (1.21 * 1.21) + abc1[1] * 1.21 + abc1[2];
                max2 = abc1[0] * (1.23 * 1.23) + abc1[1] * 1.23 + abc1[2];
                max3 = abc1[0] * (1.42 * 1.42) + abc1[1] * 1.42 + abc1[2];

                values.cC.Add(Math.Round((max1 - min1) / (0.5) * (0.0) + min1, 3));
                values.cC.Add(Math.Round((max2 - min2) / (0.5) * (0.0) + min2, 3));
                values.cC.Add(Math.Round((max3 - min3) / (0.5) * (0.0) + min3, 3));

            }

            
        }
        //Время 
        void dryngTime(DataBaseViewModel dataBase, ValuesCalculation values)
        {
            double time, result = 0;
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
                values.cTime.Add(Math.Round(time));
            }
            values.time = values.cTime.ToArray().Sum();
            //for (int j = 0; j < 3; j++)
            //    result += Convert.ToDouble(dataComponent.Rows[j].Cells[14].Value);
        }
    }
}
