using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private DryingWood_DBContext dBContext;
        public HomeController(ILogger<HomeController> logger, DryingWood_DBContext context)
        {
            _logger = logger;
            dBContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Index(PlanDrying planDrying, DataBaseViewModel dataBase)
        {
            int timeNow = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) * dataBase.CamerValue;
            planDrying.ValueChamber = dataBase.CamerValue;
            planDrying.MonthDrying = DateTime.Now.ToString("MMMM");
            planDrying.Utility = 0;
            planDrying.HoursLeftDrying = timeNow * 24;
            planDrying.HoursSpendDrying = planDrying.HoursLeftDrying - timeNow * 24;
            dBContext.PlanDryings.Add(planDrying);
            await dBContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }
        //public async Task<IActionResult> Calculation()
        //{

        //    return View();
        //}

        public async Task<IActionResult> Calculation(Calculation calc, DataBaseViewModel dataBase, ValuesCalculation values, ValuesTableCCalculation tableC, ValueWood valueWood, ChamberWood chamberWood, Chamber chamber)
        {
            int timeNow = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
           
            values.Values(calc, dataBase, values, tableC);
            valueWood.StartWetness = dataBase.StartDamp;
            valueWood.EndWetness = dataBase.EndDamp;
            valueWood.StartWidth = (int)dataBase.S1;
            valueWood.EndWidth = (int)dataBase.S2;
            valueWood.TypeWood = dataBase.TreeSpecies;
            valueWood.HoursWood = (int)values.time;
            dBContext.ValueWoods.Add(valueWood);
            await dBContext.SaveChangesAsync();
            chamberWood.ValueId = valueWood.Id;
            chamberWood.TypeWood = dataBase.TreeSpecies;
            chamberWood.TimeHours = (int)values.time;
            chamberWood.Position = 1;//Отредактировать
            dBContext.ChamberWoods.Add(chamberWood);
            await dBContext.SaveChangesAsync();
            chamber.ChamberWoodId = chamberWood.Id;
            chamber.ChamberHoursLeft = timeNow * 24;
            chamber.ChamberHoursSpend += (int)values.time;
            PlanDrying plan = await dBContext.PlanDryings.FirstOrDefaultAsync(p => p.Id == 9);
            chamber.PlanDryingId = plan.Id;
            dBContext.Chambers.Add(chamber);
            await dBContext.SaveChangesAsync();



            return View(values);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
