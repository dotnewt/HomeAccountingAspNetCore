using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeAccountingWithAspNet.Data;
using HomeAccountingWithAspNet.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using HomeAccountingWithAspNet.Models.ViewModels;

namespace HomeAccountingWithAspNet.Controllers
{
    public class IncomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public IncomeController(ApplicationDbContext db)
        {
            _db = db;
            
        }
        private IncomeMV IncomeMV()
        {
            IncomeMV incomeMV = new IncomeMV()
            {
                Income = new Income(),
                TypeDropDown = _db.IncomeCategories.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            return incomeMV;
        }

        public IActionResult Index()
        {
            IEnumerable<Income> objList = _db.Incomes;

            foreach (var obj in objList.ToList())
            {
                obj.Category = _db.IncomeCategories.FirstOrDefault(u => u.Id == obj.IncomeTypeId);
            }
            return View(objList.OrderBy(objlist => objlist.dateTime));
        }

        [HttpPost]
        public JsonResult GetWeeklyIncome()
        {
            List<object> chartData = new List<object>();
            chartData.Add(new object[]
            {
                "Категория", "Сумма" 
            });
            foreach(var item in _db.IncomeCategories.ToList())
            {
                chartData.Add(new object[]
                {
                    $"{item.Name}", _db.Incomes.Where(cat => cat.Category.Name == $"{item.Name}" && (cat.dateTime > DateTime.Now.AddDays(-7) && cat.dateTime < DateTime.Now.AddDays(1))).Select(cat => cat.Amount).Sum()
                  
                });
            }
            return Json(chartData);
        }

        [HttpPost]
        public JsonResult GetMonthlyIncome()
        {
            List<object> chartData = new List<object>();
            chartData.Add(new object[]
            {
                "Категория", "Сумма"
            });
            foreach (var item in _db.IncomeCategories.ToList())
            {
                chartData.Add(new object[]
                {
                    $"{item.Name}", _db.Incomes.Where(cat => cat.Category.Name == $"{item.Name}" && (cat.dateTime > DateTime.Now.AddDays(-DateTime.Today.Day))).Select(cat => cat.Amount).Sum()
                });
            }
            return Json(chartData);
        }

        public IActionResult IncomeReport()
        {
            return View();
        }

        // GET-Create
        public IActionResult Create()
        {
            return View(IncomeMV());
        }

        // POST-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IncomeMV obj)
        {
            if (ModelState.IsValid)
            {
                _db.Incomes.Add(obj.Income);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(IncomeMV());
        }

        // GET-Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Incomes.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // POST-Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Incomes.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            _db.Incomes.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET-Update
        public IActionResult Update(int? id)
        {
            IncomeMV income = IncomeMV();
            if (id == null || id == 0)
            {
                return NotFound();
            }
            income.Income = _db.Incomes.Find(id);
            if(income.Income == null)
            {
                return NotFound();
            }
            return View(income);
        }

        // POST-Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(IncomeMV obj)
        {
            if (ModelState.IsValid)
            {
                _db.Incomes.Update(obj.Income);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(IncomeMV());
        }
    }
}
