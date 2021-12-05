using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleWebApp.Data;
using SimpleWebApp.Models;

namespace SimpleWebApp.Controllers
{
    public class TodoController : Controller
    {
        private readonly ILogger<TodoController> _logger;
        private readonly AppDbContext _dbContext;

        public TodoController(ILogger<TodoController> logger, AppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        //get Database Item and return to view
        public IActionResult Index()
        {
            //ViewBag.Error = TempData["ModelState"];
            //var items =_dbContext.TodoItems.ToList();
            List<TodoItem> items = _dbContext.TodoItems.ToList();
            return View(items); 
        
        }


        [Authorize]
        //get Add View
        public IActionResult Add()
        {
            return View();
        }
        
        
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]

        //take input from user and post to database
        public IActionResult Add(TodoItem model) 
        {
            try
            {
                if (!ModelState.IsValid) return View(); 
                _dbContext.TodoItems.Add(model);
                _dbContext.SaveChanges();
 
                ViewBag.Success = "Todo Item Successfully Added";
                return View();
            }
            catch (Exception ex)
            {
                //throw; 
                ModelState.AddModelError(ex.Message, null);
                return View();
            }
            
        }

        [Authorize]
        //get SingleItem View
        public IActionResult SingleItem(int ID) 
        {
            try
            {
                if(ID <= 0) return RedirectToAction("Index");

                var item = _dbContext.TodoItems.FirstOrDefault(x => x.ID == ID);
                //if (item == null) return RedirectToAction("Index");
                if (item == null)
                {
                    ViewBag.ErrorMessage = $"No Todo Item with Id {ID} in the database";
                    return View();
                }

                return View(item); 
            } 
            catch (Exception ex)
            {

                //throw; 
                ModelState.AddModelError(ex.Message, null);
                return View();
            }
            
        }

        [Authorize]
        public IActionResult EditItem(int ID) 
        {
            try
            {
                if(ID <= 0) return RedirectToAction("Index");

                var item = _dbContext.TodoItems.FirstOrDefault(x => x.ID == ID);
                if (item == null)
                {
                    ViewBag.ErrorMessage = $"No Todo Item with Id {ID} in the database";
                    return View();
                }

                return View(item); 
            } 
            catch (Exception ex)
            {

                //throw; 
                ModelState.AddModelError(ex.Message, null);
                return View();
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        //take Edited input from user and Update to database
        public IActionResult EditItem(TodoItem model) 
        {
            try
            {
                if (!ModelState.IsValid) return View(); 

                var item = _dbContext.TodoItems.FirstOrDefault(x => x.ID == model.ID);
                if (item == null)
                {
                    ViewBag.ErrorMessage = $"No Todo Item with Id {model.ID} in the database";
                    return View();
                }

                item.Name = model.Name;
                item.Description = model.Description;
                item.DueDate = model.DueDate;

                _dbContext.TodoItems.Update(item);
                _dbContext.SaveChanges();
 
                ViewBag.Success2 = "Todo Item Successfully Updated";
                return View(item);
            }
            catch (Exception ex)
            {
                //throw; 
                ModelState.AddModelError(ex.Message, null);
                return View();
            }
            
        }
        

        [Authorize]
        //Delete an Item From the Database
        public IActionResult DeleteItem(int ID) 
        {
            try
            {
                if(ID <= 0) return RedirectToAction("Index");

                var item = _dbContext.TodoItems.FirstOrDefault(x => x.ID == ID);
                if (item == null)
                {
                    ViewBag.ErrorMessage = $"No Todo Item with Id {ID} in the database";
                    return View();
                }

                return View(item); 
            } 
            catch (Exception ex)
            {

                //throw; 
                ModelState.AddModelError(ex.Message, null);
                return View();
            }
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        //Delete Data from the Database
        public IActionResult DeleteItem(TodoItem model) 
        {
            try
            {
                if (!ModelState.IsValid) return View(); 

                var item = _dbContext.TodoItems.FirstOrDefault(x => x.ID == model.ID);
                if (item == null)
                {
                    ViewBag.ErrorMessage = $"No Todo Item with Id {model.ID} in the database";
                    return View();
                }

              

                _dbContext.TodoItems.Remove(item);
                _dbContext.SaveChanges();
 
                ViewBag.Success3 = "Deleted Succesfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //throw; 
                ModelState.AddModelError(ex.Message, null);
                return View();
            }
            
        }

 
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore=true)]

        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id?? HttpContext.TraceIdentifier});
        }

        //private IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}