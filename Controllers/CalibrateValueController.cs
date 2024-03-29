﻿using Microsoft.AspNetCore.Http;
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
                
                i.HoursLeftDrying -= i.HoursSpendDrying;
            }
            return View(await dBContext.PlanDryings.ToListAsync());
        }

        //private IQueryable chamberListWood(int? id)
        //{
        //    var chamber = dBContext.Chambers.Where(p => p.PlanDryingId == id);

        //    return chamber;
        //}
        // GET: CalibrateValueController/Details/ План загруженности сушильных камер
        public async Task<IActionResult> Details(int? id, DetailsPlanViewModel details)
        {
            if(id != null)
            {
                List<double> chamberCapacity = new List<double>();
                List<double> chamberHoursSpend = new List<double>();
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
                    for(int i = 1; i <= plan.ValueChamber; i++)
                    {
                        double values = 0;
                        double hoursSpend = 0;
                        var index = details.chambers.Where(p => p.ChamberNumber == i);
                        foreach(var j in index)
                        {
                            hoursSpend += (double)Math.Ceiling(j.ChamberHoursSpend / Convert.ToDecimal(24));
                            values += (double)j.ChamberCapacity;
                        }
                        chamberHoursSpend.Add(hoursSpend);
                        chamberCapacity.Add(values);
                    }
                    details.chamberHoursSpend = chamberHoursSpend;
                    details.chambCapacity = chamberCapacity;
                    foreach (var cap in details.chambers){
                        details.capacity += (double)cap.ChamberCapacity;
                        details.dateYers = DateTime.ParseExact(cap.PlanDrying.MonthDrying, "MMMM", null).ToString("MMMM.yyy");
                        details.date = DateTime.ParseExact(cap.PlanDrying.MonthDrying, "MMMM", null);
                    }
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
        public async Task<IActionResult> Calculation(Calculation calc, DataBaseViewModel dataBase, ValuesCalculation values, 
            ValuesTableCCalculation tableC, ValueWood valueWood, ChamberWood chamberWood, Chamber chamber)
        {
            int timeNow = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            values.Values(calc, dataBase, values, tableC);
            Chamber s = await dBContext.Chambers.FirstOrDefaultAsync(p => p.PlanDryingId == dataBase.PlanID);
            int valueHours = s.ChamberHoursLeft;
            int valueFact = 0;
            foreach(var p in dBContext.Chambers.Where(p => p.ChamberNumber == dataBase.CameraId && p.PlanDryingId == dataBase.PlanID))
            {
                valueFact += p.ChamberHoursSpend;
            }
            if(valueHours >= valueFact + values.time)
            {
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
                chamber.ChamberCapacity = Convert.ToDecimal(values.chCapacity);
                dBContext.Chambers.Add(chamber);
                await dBContext.SaveChangesAsync();

                return View(values);
            }
            else
            {
                return NotFound();
            }
            
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

        // POST: CalibrateValueController/Create

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                PlanDrying plan = await dBContext.PlanDryings.FirstOrDefaultAsync(p => p.Id == id);
                if (plan != null)
                    return View(plan);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                PlanDrying user = await dBContext.PlanDryings.FirstOrDefaultAsync(p => p.Id == id);
                if (user != null)
                {
                    dBContext.PlanDryings.Remove(user);
                    await dBContext.SaveChangesAsync();
                    return RedirectToAction("PrintValue");
                }
            }
            return NotFound();
        }

        
    }
}
