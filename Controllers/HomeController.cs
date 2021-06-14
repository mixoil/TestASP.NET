using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestASP.NET.Models;

namespace TestASP.NET.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationContext db;
        private Dictionary<string, string> countries = new Dictionary<string, string>()
        {
            {"USA", "США" },
            {"Italy", "Италия" },
            {"Russia", "Россия" }
        };
        private Dictionary<string, string> genders = new Dictionary<string, string>()
        {
            {"Male", "Мужской" },
            {"Female", "Женский" },
        };

        public HomeController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult AddFootballer()
        {
            ViewBag.Countries = countries;
            ViewBag.Genders = genders;
            ViewBag.Teams = db.Footballers.Select(f => f.Team).Distinct();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFootballer(Footballer footballer)
        {
            if (ModelState.IsValid)
            {
                db.Footballers.Add(footballer);
                await db.SaveChangesAsync();
                return RedirectToAction("FootballersList");
            }
            else
                return View(footballer);
        }

        [HttpGet]
        [Route("~/EditFootballer")]
        public IActionResult EditFootballer(int id)
        {
            ViewBag.Countries = countries;
            ViewBag.Genders = genders;
            ViewBag.Teams = db.Footballers.Select(f => f.Team).Distinct();
            ViewBag.Footballer = db.Footballers.Find(id);
            return View();
        }

       [HttpPost]
       [Route("~/EditFootballer")]
        public async Task<IActionResult> EditFootballer(Footballer footballer)
        {
            if (ModelState.IsValid)
            {
                var dbFootballer = db.Footballers.SingleOrDefault(f => f.Id == footballer.Id);
                if(dbFootballer != null)
                {
                    foreach(var prop in typeof(Footballer).GetProperties())
                        prop.SetValue(dbFootballer, prop.GetValue(footballer));
                    await db.SaveChangesAsync();
                }
                return RedirectToAction("FootballersList");
            }
            else
                return View(footballer);
        }

        [Route("~/FootballersList")]
        public async Task<IActionResult> FootballersList()
        {
            ViewBag.Countries = countries;
            ViewBag.Genders = genders;
            return View(await db.Footballers.ToListAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
