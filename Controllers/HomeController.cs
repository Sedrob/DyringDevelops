using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Test.Formulas;
using Test.Models;

namespace Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        //public IActionResult Calculation()
        //{

        //    return View();
        //}

        public IActionResult Calculation(Calculation calc, DataBaseViewModel dataBase, ValuesCalculation values, ValuesTableCCalculation tableC)
        {
            values.Values(calc, dataBase, values, tableC);

            return View(values);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }











        // Ввод значений
        //[HttpGet]
        //public IActionResult PrintValue()
        //{
        //    int age = 16;
        //    char name = 'g';
        //    //var user = new User { Name = name, Age = age };
        //    return  View(age);
        //}

        //[HttpGet]
        //public IActionResult PrintValuesCollection()
        //{
        //    var numberList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        //    var numberArray = new string[] { "1", "2", "3" };

        //    return View(numberList);
        //}

        //[HttpGet]
        //public IActionResult CreateUser() => View();

        //[HttpPost]
        //public async Task<IActionResult> CreateUser(CreateUserViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await _userSevice.AddUser
        //    }
        //}
    }
}
