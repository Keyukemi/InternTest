using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleWebApp.Models;

namespace SimpleWebApp.Controllers
{
    public class SimpleInterestController : Controller
    {
        private readonly ILogger<SimpleInterestController> _logger;

        public SimpleInterestController(ILogger<SimpleInterestController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(SimpleInterestModel model)
        {
            try
            {
                if (!ModelState.IsValid) return View();

                int interest = (int.Parse(model.Price) * 10 * int.Parse(model.Time)*12)/100;

                //ViewBag.Interest = interest;

                SimpleInterestModel vm = new SimpleInterestModel()
                {
                    Price = model.Price,
                    Time = model.Time,
                    Interest = interest.ToString(),
                };
                return RedirectToAction("Interest", vm);
                
            }
            catch (Exception ex)
            {
                
                //throw;
                ModelState.AddModelError(ex.Message, null);
                return View();
            }
        }

        [HttpGet]
        public IActionResult Interest(SimpleInterestModel model)
        {
            return View(model);
        }

        // [HttpPost]
        // public IActionResult Interest(SimpleInterestModel model)
        // {
        //     try
        //     {
        //         if (!ModelState.IsValid) return RedirectToAction("Index");

        //         var interest = (int.Parse(model.Price) * 10 * int.Parse(model.Time))/100;

        //         //ViewBag.Interest = interest;

        //         SimpleInterestModel vm = new SimpleInterestModel()
        //         {
        //             Price = model.Price,
        //             Time = model.Time, 
        //             Interest = interest.ToString()
        //         };
        //         return View(vm);
                
        //     }
        //     catch (Exception ex)
        //     {
                
        //         //throw;
        //         ModelState.AddModelError(ex.Message, null);
        //         return View();
        //     }
        // }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }  
    }
}