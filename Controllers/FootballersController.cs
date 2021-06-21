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
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.SignalR;

namespace TestASP.NET.Controllers
{
    public class FootballersController : Controller
    {
        private ApplicationContext db;
        private IHubContext<FootballersHub> hub;

        public FootballersController(ApplicationContext context, IHubContext<FootballersHub> hub)
        {
            this.hub = hub;
            db = context;
        }

        [HttpGet]
        public IActionResult GetFootballersTableContent()
        {
            return PartialView("_FootballersTableContent", db.Footballers.ToList());
        }

        [HttpGet]
        public IActionResult AddFootballer()
        {
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
                await hub.Clients.All.SendAsync("UpdatePage");
                return RedirectToAction("FootballersList");
            }
            else
                return View(footballer);
        }

        [HttpGet]
        [Route("~/EditFootballer")]
        public IActionResult EditFootballer(int id)
        {
            ViewBag.Teams = db.Footballers.Select(f => f.Team).Distinct();
            var footballer = db.Footballers.Find(id);
            return View(footballer);
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
                    foreach(var prop in typeof(Footballer).GetProperties().Where(p => p.Name != "Id"))
                        prop.SetValue(dbFootballer, prop.GetValue(footballer));
                    await db.SaveChangesAsync();
                    await hub.Clients.All.SendAsync("UpdatePage");
                }
                return RedirectToAction("FootballersList");
            }
            else
                return View(footballer);
        }

        public IActionResult FootballersList()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
