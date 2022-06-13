using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Formulas;
using Test.Models;
using Test.ViewModels;

namespace Test.Controllers
{
    public class CalibrateValueController : Controller
    {
        private readonly ILogger<CalibrateValueController> _logger;
        private DryingWood_DBContext dBContext;
        public CalibrateValueController(ILogger<CalibrateValueController> logger, DryingWood_DBContext context)
        {
            _logger = logger;
            dBContext = context;
        }
        // GET: CalibrateValueController 
        //Получение всех планов
        public async Task<IActionResult> PrintValue()
        {
            foreach(var i in await dBContext.PlanDryings.ToListAsync())
            {
                int chois = 0;
                foreach (var j in dBContext.Chambers.Where(p => p.PlanDryingId == i.Id))
                {
                    chois += j.ChamberHoursSpend;
                }
                i.HoursSpendDrying = chois;
                i.Utility = Math.Round((decimal)i.HoursSpendDrying / (decimal)i.HoursLeftDrying * 100) ;
                //if(chois != 0)
                //{
                //    i.Utility = i.HoursLeftDrying / i.HoursSpendDrying;
                //}
                i.HoursLeftDrying -= i.HoursSpendDrying;
                
            }
            return View(await dBContext.PlanDryings.ToListAsync());
        }

        private IQueryable chamberListWood(int? id)
        {
            var chamber = dBContext.Chambers.Where(p => p.PlanDryingId == id);

            return chamber;
        }
        // GET: CalibrateValueController/Details/5 План загруженности сушильных камер
        public async Task<IActionResult> Details(int? id, DetailsPlanViewModel details)
        {
            if(id != null)
            {
                PlanDrying plan = await dBContext.PlanDryings.FirstOrDefaultAsync(p => p.Id == id);
                if (plan != null)
                {
                    details.chambers = dBContext.Chambers.Where(p => p.PlanDryingId == id)
                    .Include(u => u.ChamberWood)
                        .ThenInclude(s => s.Value)
                    .Include(u => u.PlanDrying)
                    .ToList(); 
                    details.plan = plan.ValueChamber;
                    HttpContext.Response.Cookies.Append("id", id.ToString());
                    return View(details);
                }
            }
            return NotFound();
        }
        //Загрузить в камеру плана древесину 
        public async Task<IActionResult> AddChambers(int? id)
        {
            PlanDrying plan = await dBContext.PlanDryings.FirstOrDefaultAsync(p => p.Id == id);
            DataBaseViewModel dataBase = new DataBaseViewModel();
            dataBase.CameraValueId = plan.ValueChamber;
            dataBase.PlanID = id;
            return View(dataBase);
        }
        // Заносение данных созданные в AddChambers
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
            chamber.ChamberNumber = dataBase.CameraId;
            chamber.ChamberWoodId = chamberWood.Id;
            chamber.ChamberHoursLeft = timeNow * 24;
            chamber.ChamberHoursSpend += (int)values.time;
            PlanDrying plan = await dBContext.PlanDryings.FirstOrDefaultAsync(p => p.Id == dataBase.PlanID);
            chamber.PlanDryingId = plan.Id;
            dBContext.Chambers.Add(chamber);
            await dBContext.SaveChangesAsync();

            return View(values);
        }
        
        public IActionResult DetailsChamber(int? id, DetailsChamberViewModel details, DataBaseViewModel dataBase, Calculation calc, ValuesCalculation values, ValuesTableCCalculation tableC)
        {
            if (id != null)
            {
                details.chambers = dBContext.Chambers
                    .Where(p => p.ChamberNumber == id && p.PlanDryingId.ToString() == HttpContext.Request.Cookies["id"])
                    .Include(u => u.ChamberWood)
                    .ThenInclude(s => s.Value)
                    .ToList();

                foreach (var chamb in details.chambers)
                {
                    dataBase.StartDamp = chamb.ChamberWood.Value.StartWetness;
                    dataBase.EndDamp = chamb.ChamberWood.Value.EndWetness;
                    dataBase.S2 = chamb.ChamberWood.Value.EndWidth;
                    dataBase.S1 = chamb.ChamberWood.Value.StartWidth;
                    dataBase.MoveAir = 1.5;
                    dataBase.numericStacks = 3;
                    dataBase.domainCirculation = "Реверсивная";
                    dataBase.Tc1 = 60;
                    dataBase.Tc2 = 65;
                    dataBase.Tc3 = 70;
                    dataBase.DtC1 = 8;
                    dataBase.DtC2 = 15;
                    dataBase.DtC3 = 25;
                    values.Values(calc, dataBase, values, tableC);
                }
                return View(values);
            }
            return NotFound();
        }

        public IActionResult OneDetailsChamber(int? id, DetailsChamberViewModel details, DataBaseViewModel dataBase, Calculation calc, ValuesCalculation values, ValuesTableCCalculation tableC)
        {
            if(id != null)
            {
                details.chambers = dBContext.Chambers
                    .Where(p => p.Id == id)
                    .Include(u => u.ChamberWood)
                    .ThenInclude(s => s.Value)
                    .ToList();
                foreach(var chamb in details.chambers)
                {
                    dataBase.StartDamp = chamb.ChamberWood.Value.StartWetness;
                    dataBase.EndDamp = chamb.ChamberWood.Value.EndWetness;
                    dataBase.S2 = chamb.ChamberWood.Value.EndWidth;
                    dataBase.S1 = chamb.ChamberWood.Value.StartWidth;
                    dataBase.TreeSpecies = chamb.ChamberWood.TypeWood;
                    dataBase.MoveAir = 1.5;
                    dataBase.numericStacks = 3;
                    dataBase.domainCirculation = "Реверсивная";
                    dataBase.Tc1 = 60;
                    dataBase.Tc2 = 65;
                    dataBase.Tc3 = 70;
                    dataBase.DtC1 = 8;
                    dataBase.DtC2 = 15;
                    dataBase.DtC3 = 25;
                    values.Values(calc, dataBase, values, tableC);
                }
                return View(values);
            }
            return NotFound();
        }
        //public async Task<IActionResult> DetailsChambers(int? id, DetailsPlanViewModel details)
        //{

        //}
        // POST: CalibrateValueController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CalibrateValueController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CalibrateValueController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CalibrateValueController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CalibrateValueController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
