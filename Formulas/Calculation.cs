using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Models;

namespace Test.Formulas
{
    public class Calculation
    {
        //Заносение Влажности Древесины Whi / Wki
        void WoodMoisture(DataBaseViewModel dataBase)
        {
            int wHi1 = dataBase.StartDamp;
            int wHi2 = 35;
            int wHi3 = 25;

            int wKi1 = 35;
            int wKi2 = 25;
            int wKi3 = dataBase.StartDamp;

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
        void EnvironmentParameters(DataBaseViewModel dataBase)
        {
            int tC1 = dataBase.Tc1;
            int tC2 = dataBase.Tc2;
            int tC3 = dataBase.Tc3;

            int tcM1 = tC1 - dataBase.DtC1;
            int tcM2 = tC2 - dataBase.DtC2;
            int tcM3 = tC3 - dataBase.DtC3;

            double f1 = Math.Round(ComputeF(tcM1, dataBase.Tc1), MidpointRounding.AwayFromZero);
            double f2 = Math.Round(ComputeF(tcM2, dataBase.Tc2), MidpointRounding.AwayFromZero);
            double f3 = Math.Round(ComputeF(tcM3, dataBase.Tc3), MidpointRounding.AwayFromZero);
        }
        ////Wp % заносение данных 
        //void PercentageHumidity()
        //{
        //    for (int i = 0; i < 3; i++)
        //    {
        //        double fi = Convert.ToInt32(dataComponent.Rows[i].Cells[5].Value);
        //        double tc = Convert.ToInt32(dataComponent.Rows[i].Cells[3].Value);
        //        double a;
        //        if (fi > 50)
        //        {
        //            a = Math.Round(0.512 / (1.21 - (fi / 100)) * (21.7 - Math.Pow(((273 + tc) / 100), 2)), 1);
        //        }
        //        else
        //        {
        //            a = Math.Round(0.36 * (13.9 - Math.Pow((273 + tc) / 100, 2)) + 0.72 * fi / 100 * (29.5 - Math.Pow((273 + tc) / 100, 2)), 1);
        //        }
        //        dataComponent.Rows[i].Cells[6].Value = a;
        //    }
        //}
        ////Заполнение Ei
        //void dryingEi()
        //{
        //    for (int i = 0; i < 3; i++)
        //    {
        //        double wh = Convert.ToInt32(dataComponent.Rows[i].Cells[1].Value);
        //        double wi = Convert.ToInt32(dataComponent.Rows[i].Cells[2].Value);
        //        double wp = Convert.ToInt32(dataComponent.Rows[i].Cells[6].Value);

        //        dataComponent.Rows[i].Cells[7].Value = Math.Round((wi - wp) / (wh - wp), 3);
        //    }
        //}
        //// Заполнение Ац и S1/S2
        //void Reversibility()
        //{
        //    for (int i = 0; i < 3; i++)
        //    {
        //        if (DataBase.domainCirculation == "Реверсивная")
        //            dataComponent.Rows[i].Cells[9].Value = 1;
        //        else
        //            dataComponent.Rows[i].Cells[9].Value = 1.1;
        //        dataComponent.Rows[i].Cells[10].Value = Math.Round(DataBase.S1 / DataBase.S2, 3);
        //    }
        //}
        //// Заполнение Aci
        //void SectionAci()
        //{
        //    for (int i = 0; i < 3; i++)
        //    {
        //        double result = 0;
        //        string tm = dataComponent.Rows[i].Cells[4].Value.ToString();
        //        if (tm[0].ToString() == "4")
        //        {
        //            if (Convert.ToInt32(tm) % 10 < 3)
        //                result = 1.7;
        //            else if (Convert.ToInt32(tm) % 10 > 7)
        //                result = 2.2;
        //            else
        //                result = 2.6;
        //        }
        //        else if (tm[0].ToString() == "5")
        //        {
        //            if (Convert.ToInt32(tm) % 10 < 3)
        //                result = 2.6;
        //            else if (Convert.ToInt32(tm) % 10 > 7)
        //                result = 3.1;
        //            else
        //                result = 3.7;
        //        }
        //        else if (tm[0].ToString() == "6")
        //        {
        //            if (Convert.ToInt32(tm) % 10 < 3)
        //                result = 3.7;
        //            else if (Convert.ToInt32(tm) % 10 > 7)
        //                result = 4.3;
        //            else
        //                result = 5.0;
        //        }
        //        else
        //            result = 5.0;
        //        dataComponent.Rows[i].Cells[8].Value = result;
        //    }

        //}
        ////Таблицы для расчетов 
        //void dataSetGreedValues()
        //{
        //    for (int i = 0; i < 3; i++)
        //        dataSetGreed.Rows.Add();
        //    dataSetGreed.Rows[0].Cells[0].Value = -0.0974;
        //    dataSetGreed.Rows[1].Cells[0].Value = -0.0554;
        //    dataSetGreed.Rows[2].Cells[0].Value = 0.9676;//1
        //    dataSetGreed.Rows[0].Cells[1].Value = -0.0866;
        //    dataSetGreed.Rows[1].Cells[1].Value = -0.0807;
        //    dataSetGreed.Rows[2].Cells[1].Value = 0.8997;//2
        //    dataSetGreed.Rows[0].Cells[2].Value = -0.1494;
        //    dataSetGreed.Rows[1].Cells[2].Value = -0.0855;
        //    dataSetGreed.Rows[2].Cells[2].Value = 0.8397;//3
        //    dataSetGreed.Rows[0].Cells[3].Value = -0.0611;
        //    dataSetGreed.Rows[1].Cells[3].Value = -0.2202;
        //    dataSetGreed.Rows[2].Cells[3].Value = 0.8057;//4
        //    dataSetGreed.Rows[0].Cells[4].Value = -0.0785;
        //    dataSetGreed.Rows[1].Cells[4].Value = -0.2097;
        //    dataSetGreed.Rows[2].Cells[4].Value = 0.7328;//5
        //    dataSetGreed.Rows[0].Cells[5].Value = 0.0227;
        //    dataSetGreed.Rows[1].Cells[5].Value = -0.3115;
        //    dataSetGreed.Rows[2].Cells[5].Value = 0.6978;//6
        //    dataSetGreed.Rows[0].Cells[6].Value = -0.0087;
        //    dataSetGreed.Rows[1].Cells[6].Value = -0.2897;
        //    dataSetGreed.Rows[2].Cells[6].Value = 0.6356;//7
        //    dataSetGreed.Rows[0].Cells[7].Value = 0.0574;
        //    dataSetGreed.Rows[1].Cells[7].Value = -0.3309;
        //    dataSetGreed.Rows[2].Cells[7].Value = 0.5882;//8
        //    dataSetGreed.Rows[0].Cells[8].Value = -0.0141;
        //    dataSetGreed.Rows[1].Cells[8].Value = -0.2637;
        //    dataSetGreed.Rows[2].Cells[8].Value = 0.5359;
        //    dataSetGreed.Rows[0].Cells[9].Value = 0.0519;
        //    dataSetGreed.Rows[1].Cells[9].Value = -0.3016;
        //    dataSetGreed.Rows[2].Cells[9].Value = -0.4988;

        //}
        //void dataSetGreedValuesc3()
        //{
        //    for (int i = 0; i < 4; i++)
        //        dataSetGreedс3.Rows.Add();
        //    dataSetGreedс3.Rows[0].Cells[0].Value = 0.5;
        //    dataSetGreedс3.Rows[1].Cells[0].Value = 0.09021;
        //    dataSetGreedс3.Rows[2].Cells[0].Value = -0.08954;
        //    dataSetGreedс3.Rows[3].Cells[0].Value = 1.02524;//1
        //    dataSetGreedс3.Rows[0].Cells[1].Value = 1;
        //    dataSetGreedс3.Rows[1].Cells[1].Value = 0.00643;
        //    dataSetGreedс3.Rows[2].Cells[1].Value = 0.57565;
        //    dataSetGreedс3.Rows[3].Cells[1].Value = 0.41698;//2
        //    dataSetGreedс3.Rows[0].Cells[2].Value = 1.5;
        //    dataSetGreedс3.Rows[1].Cells[2].Value = 0.06576;
        //    dataSetGreedс3.Rows[2].Cells[2].Value = 0.69059;
        //    dataSetGreedс3.Rows[3].Cells[2].Value = 0.24478;//3
        //    dataSetGreedс3.Rows[0].Cells[3].Value = 2;
        //    dataSetGreedс3.Rows[1].Cells[3].Value = 0.13958;
        //    dataSetGreedс3.Rows[2].Cells[3].Value = 0.66026;
        //    dataSetGreedс3.Rows[3].Cells[3].Value = 0.23500;//4
        //    dataSetGreedс3.Rows[0].Cells[4].Value = 2.5;
        //    dataSetGreedс3.Rows[1].Cells[4].Value = 0.02359;
        //    dataSetGreedс3.Rows[2].Cells[4].Value = 1.48797;
        //    dataSetGreedс3.Rows[3].Cells[4].Value = -0.50543;//5
        //    dataSetGreedс3.Rows[0].Cells[5].Value = 3;
        //    dataSetGreedс3.Rows[1].Cells[5].Value = 0.25026;
        //    dataSetGreedс3.Rows[2].Cells[5].Value = 0.99284;
        //    dataSetGreedс3.Rows[3].Cells[5].Value = -0.20439;//6
        //}
        ////Расчет последних 
        //void ct()
        //{
        //    dataSetGreedValues();
        //    double a, b, c, a1, b1, c1 = 0;
        //    double min1, min2, min3, max1, max2, max3 = 0;

        //    string s1s2 = dataComponent.Rows[0].Cells[10].Value.ToString();
        //    double s1s2int = Math.Round(Convert.ToDouble(dataComponent.Rows[0].Cells[10].Value), 1);
        //    if (s1s2.Length == 3)
        //    {
        //        for (int k = 0; k < 10; k++)
        //        {
        //            if (dataSetGreed.Columns[k].DisplayIndex == s1s2int * 10)
        //            {
        //                a = Convert.ToDouble(dataSetGreed.Rows[0].Cells[k].Value);
        //                b = Convert.ToDouble(dataSetGreed.Rows[1].Cells[k].Value);
        //                c = Convert.ToDouble(dataSetGreed.Rows[2].Cells[k].Value);

        //                dataComponent.Rows[0].Cells[11].Value = Math.Round(a * (0.35 * 0.35) + b * 0.35 + c, 4);
        //                dataComponent.Rows[1].Cells[11].Value = Math.Round(a * (0.4865 * 0.4865) + b * 0.4865 + c, 4);
        //                dataComponent.Rows[2].Cells[11].Value = Math.Round(a * (0.2584 * 0.2584) + b * 0.2584 + c, 4);

        //                break;
        //            }
        //        }
        //    }
        //    else if (s1s2.Length > 3)
        //    {
        //        if (Convert.ToInt32(s1s2[3]) < 5)
        //        {
        //            for (int k = 0; k < 10; k++)
        //            {
        //                if (s1s2int * 10 == dataSetGreed.Columns[k].DisplayIndex)
        //                {
        //                    a = Convert.ToDouble(dataSetGreed.Rows[0].Cells[k - 1].Value);
        //                    b = Convert.ToDouble(dataSetGreed.Rows[1].Cells[k - 1].Value);
        //                    c = Convert.ToDouble(dataSetGreed.Rows[2].Cells[k - 1].Value);

        //                    a1 = Convert.ToDouble(dataSetGreed.Rows[0].Cells[k].Value);
        //                    b1 = Convert.ToDouble(dataSetGreed.Rows[1].Cells[k].Value);
        //                    c1 = Convert.ToDouble(dataSetGreed.Rows[2].Cells[k].Value);

        //                    min1 = a * (0.35 * 0.35) + b * 0.35 + c;
        //                    min2 = a * (0.4865 * 0.4865) + b * 0.4865 + c;
        //                    min3 = a * (0.2584 * 0.2584) + b * 0.2584 + c;

        //                    max1 = a1 * (0.35 * 0.35) + b1 * 0.35 + c1;
        //                    max2 = a1 * (0.4865 * 0.4865) + b1 * 0.4865 + c1;
        //                    max3 = a1 * (0.2584 * 0.2584) + b1 * 0.2584 + c1;

        //                    dataComponent.Rows[0].Cells[11].Value = Math.Round((max1 - min1) / (0.1) * (0.0) + min1, 4);
        //                    dataComponent.Rows[1].Cells[11].Value = Math.Round((max2 - min2) / (0.1) * (0.0) + min2, 4);
        //                    dataComponent.Rows[2].Cells[11].Value = Math.Round((max3 - min3) / (0.1) * (0.0) + min3, 4);
        //                    break;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            for (int k = 1; k <= 10; k++)
        //            {
        //                if (s1s2int * 10 == dataSetGreed.Columns[k].DisplayIndex)
        //                {
        //                    a = Convert.ToDouble(dataSetGreed.Rows[0].Cells[k - 2].Value);
        //                    b = Convert.ToDouble(dataSetGreed.Rows[1].Cells[k - 2].Value);
        //                    c = Convert.ToDouble(dataSetGreed.Rows[2].Cells[k - 2].Value);

        //                    a1 = Convert.ToDouble(dataSetGreed.Rows[0].Cells[k - 1].Value);
        //                    b1 = Convert.ToDouble(dataSetGreed.Rows[1].Cells[k - 1].Value);
        //                    c1 = Convert.ToDouble(dataSetGreed.Rows[2].Cells[k - 1].Value);

        //                    min1 = Math.Round(a * (0.35 * 0.35) + b * 0.35 + c, 4);
        //                    min2 = Math.Round(a * (0.4865 * 0.4865) + b * 0.4865 + c, 4);
        //                    min3 = Math.Round(a * (0.2584 * 0.2584) + b * 0.2584 + c, 4);

        //                    max1 = Math.Round(a1 * (0.35 * 0.35) + b1 * 0.35 + c1, 4);
        //                    max2 = Math.Round(a1 * (0.4865 * 0.4865) + b1 * 0.4865 + c1, 4);
        //                    max3 = Math.Round(a1 * (0.2584 * 0.2584) + b1 * 0.2584 + c1, 4);

        //                    dataComponent.Rows[0].Cells[11].Value = Math.Round((max1 - min1) / (0.1) * (0.1) + min1, 4);
        //                    dataComponent.Rows[1].Cells[11].Value = Math.Round((max2 - min2) / (0.1) * (0.1) + min2, 4);
        //                    dataComponent.Rows[2].Cells[11].Value = Math.Round((max3 - min3) / (0.1) * (0.1) + min3, 4);
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //}
        //void ctB()
        //{
        //    dataComponent.Rows[0].Cells[12].Value = Math.Round(Convert.ToDouble(dataComponent.Rows[0].Cells[11].Value) *
        //        ((65 * Math.Pow(DataBase.S1 / 10, 2) / Convert.ToDouble(dataComponent.Rows[0].Cells[8].Value)) * 1.31), 1);
        //    dataComponent.Rows[1].Cells[12].Value = Math.Round(Convert.ToDouble(dataComponent.Rows[1].Cells[11].Value) *
        //        ((65 * Math.Pow(DataBase.S1 / 10, 2) / Convert.ToDouble(dataComponent.Rows[1].Cells[8].Value)) * 1.31), 1);
        //    dataComponent.Rows[2].Cells[12].Value = Math.Round(Convert.ToDouble(dataComponent.Rows[2].Cells[11].Value) *
        //        ((65 * Math.Pow(DataBase.S1 / 10, 2) / Convert.ToDouble(dataComponent.Rows[2].Cells[8].Value)) * 1.31), 1);
        //}
        //void c3()
        //{
        //    dataSetGreedValuesc3();
        //    double a, b, c, a1, b1, c1 = 0;
        //    double min1, min2, min3, max1, max2, max3 = 0;
        //    for (int k = 0; k < 6; k++)
        //    {
        //        if (DataBase.numericStacks == Convert.ToInt32(dataSetGreedс3.Rows[0].Cells[k].Value))
        //        {
        //            if (DataBase.numericStacks == 3)
        //            {
        //                a = Convert.ToDouble(dataSetGreedс3.Rows[1].Cells[k].Value);
        //                b = Convert.ToDouble(dataSetGreedс3.Rows[2].Cells[k].Value);
        //                c = Convert.ToDouble(dataSetGreedс3.Rows[3].Cells[k].Value);

        //                min1 = a * (1.21 * 1.21) + b * 1.21 + c;
        //                min2 = a * (1.23 * 1.23) + b * 1.23 + c;
        //                min3 = a * (1.42 * 1.42) + b * 1.42 + c;

        //                max1 = a * (1.21 * 1.21) + b * 1.21 + c;
        //                max2 = a * (1.23 * 1.23) + b * 1.23 + c;
        //                max3 = a * (1.42 * 1.42) + b * 1.42 + c;

        //                dataComponent.Rows[0].Cells[13].Value = Math.Round((max1 - min1) / (0.5) * (0.0) + min1, 3);
        //                dataComponent.Rows[1].Cells[13].Value = Math.Round((max2 - min2) / (0.5) * (0.0) + min2, 3);
        //                dataComponent.Rows[2].Cells[13].Value = Math.Round((max3 - min3) / (0.5) * (0.0) + min3, 3);

        //                break;
        //            }
        //            else
        //            {
        //                a = Convert.ToDouble(dataSetGreedс3.Rows[1].Cells[k].Value);
        //                b = Convert.ToDouble(dataSetGreedс3.Rows[2].Cells[k].Value);
        //                c = Convert.ToDouble(dataSetGreedс3.Rows[3].Cells[k].Value);

        //                a1 = Convert.ToDouble(dataSetGreedс3.Rows[1].Cells[k + 1].Value);
        //                b1 = Convert.ToDouble(dataSetGreedс3.Rows[2].Cells[k + 1].Value);
        //                c1 = Convert.ToDouble(dataSetGreedс3.Rows[3].Cells[k + 1].Value);

        //                min1 = a * (1.21 * 1.21) + b * 1.21 + c;
        //                min2 = a * (1.23 * 1.23) + b * 1.23 + c;
        //                min3 = a * (1.42 * 1.42) + b * 1.42 + c;

        //                max1 = a1 * (1.21 * 1.21) + b1 * 1.21 + c1;
        //                max2 = a1 * (1.23 * 1.23) + b1 * 1.23 + c1;
        //                max3 = a1 * (1.42 * 1.42) + b1 * 1.42 + c1;

        //                dataComponent.Rows[0].Cells[13].Value = Math.Round((max1 - min1) / (0.5) * (0.0) + min1, 3);
        //                dataComponent.Rows[1].Cells[13].Value = Math.Round((max2 - min2) / (0.5) * (0.0) + min2, 3);
        //                dataComponent.Rows[2].Cells[13].Value = Math.Round((max3 - min3) / (0.5) * (0.0) + min3, 3);

        //                break;
        //            }

        //        }
        //    }
        //}
        ////Время 
        //void dryngTime()
        //{
        //    double time, result = 0;
        //    for (int i = 0; i < 3; i++)
        //    {
        //        if (1 / Convert.ToDouble(dataComponent.Rows[i].Cells[7].Value) < 1.4)
        //        {
        //            time = Convert.ToDouble(dataComponent.Rows[i].Cells[11].Value) * 65 * Math.Pow(DataBase.S1 / 10, 2) /
        //                Convert.ToDouble(dataComponent.Rows[i].Cells[8].Value) * Convert.ToDouble(dataComponent.Rows[i].Cells[13].Value) *
        //                Convert.ToDouble(dataComponent.Rows[i].Cells[9].Value) * Math.Log10(1 / Convert.ToDouble(dataComponent.Rows[i].Cells[7].Value));
        //        }
        //        else
        //        {
        //            time = Convert.ToDouble(dataComponent.Rows[i].Cells[11].Value) * 65 * Math.Pow(DataBase.S1 / 10, 2) /
        //                Convert.ToDouble(dataComponent.Rows[i].Cells[8].Value) * Convert.ToDouble(dataComponent.Rows[i].Cells[13].Value) *
        //                Convert.ToDouble(dataComponent.Rows[i].Cells[9].Value) * 0.81 * Math.Log10(1 / Convert.ToDouble(dataComponent.Rows[i].Cells[7].Value));
        //        }
        //        dataComponent.Rows[i].Cells[14].Value = Math.Round(time);
        //    }
        //    for (int j = 0; j < 3; j++)
        //        result += Convert.ToDouble(dataComponent.Rows[j].Cells[14].Value);

        //    textResult.Text = result.ToString();
        //}
    }
}
