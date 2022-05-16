using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Formulas
{
    public class ValuesTableCCalculation
    {
        public List<double> GreedValues(double x)
        {
            List<double> greed = new List<double>();

            switch (x)
            {
                case 0.1:
                    greed.AddRange(new[] { -0.0974, -0.0554, 0.9676 });
                    return greed;
                case 0.2:
                    greed.AddRange(new[] { -0.0866, -0.0807, 0.8997 });
                    return greed;
                case 0.3:
                    greed.AddRange(new[] { -0.1494, -0.0855, 0.8397 });
                    return greed;
                case 0.4:
                    greed.AddRange(new[] { -0.0611, -0.2202, 0.8057 });
                    return greed;
                case 0.5:
                    greed.AddRange(new[] { -0.0785, -0.2097, 0.7328 });
                    return greed;
                case 0.6:
                    greed.AddRange(new[] { 0.0227, -0.3115, 0.6978 });
                    return greed;
                case 0.7:
                    greed.AddRange(new[] { -0.0087, -0.2897, 0.6356 });
                    return greed;
                case 0.8:
                    greed.AddRange(new[] { 0.0574, -0.3309, 0.5882 });
                    return greed;
                case 0.9:
                    greed.AddRange(new[] { -0.0141, -0.2637, 0.5359 });
                    return greed;
                case 1:
                    greed.AddRange(new[] { 0.0519, -0.3016, -0.4988 });
                    return greed;
            }
            return greed;
        }

        public List<double> GreedValuesC3(double x)
        {
            List<double> greedC = new List<double>();

            switch (x)
            {
                case 0.5:
                    greedC.AddRange(new[] { 0.09021, -0.08954, 1.02524 });
                    return greedC;
                case 1:
                    greedC.AddRange(new[] { 0.006576, 0.57565, 0.41698 });
                    return greedC;
                case 1.5:
                    greedC.AddRange(new[] { 0.065756, 0.69059, 0.24478 });
                    return greedC;
                case 2:
                    greedC.AddRange(new[] { 0.13958, 0.66026, 0.235 });
                    return greedC;
                case 2.5:
                    greedC.AddRange(new[] { 0.02359, 1.48797, -0.50543 });
                    return greedC;
                case 3:
                    greedC.AddRange(new[] { 0.25026, 0.99284, -0.20439 });
                    return greedC;
            }
            return greedC;
        }
    }
}
