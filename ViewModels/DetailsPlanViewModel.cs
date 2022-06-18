using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.ViewModels
{
    public class DetailsPlanViewModel
    {
        public IEnumerable<Chamber> chambers { get; set; }
        public List<double> chamberHoursSpend { get; set; }
        public int plan { get; set; }
        public double capacity { get; set; }
        public string dateYers { get; set; }
        public DateTime date { get; set; }
        public List<double> chambCapacity {get; set;}


}
}
