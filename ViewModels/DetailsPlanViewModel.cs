using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.ViewModels
{
    public class DetailsPlanViewModel
    {
        public IEnumerable<Chamber> chambers { get; set; }
        public int plan { get; set; }

    }
}
