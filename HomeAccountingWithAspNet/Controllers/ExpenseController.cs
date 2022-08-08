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
    public class ExpenseController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ExpenseController(ApplicationDbContext db)
        {
            _db = db;
        }
        private ExpenseMV ExpenseMV()
        {
            ExpenseMV expenseMV = new ExpenseMV()
            {
                Expense = new Expense(),
                TypeDropDown = _db.ExpenseCategories.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            return expenseMV;
        }

        public IActionResult Index()
        {
            IEnumerable<Expense> objList = _db.Expenses;
            foreach (var obj in objList.ToList())
            {
                obj.Category = _db.ExpenseCategories.FirstOrDefault(u => u.Id == obj.ExpenseTypeId);
            }
            return View(objList.OrderBy(objlist => objlist.dateTime));
        }

        //[Route("Expense/Index")]
        [HttpPost]
        public JsonResult GetWeeklyExpense()
        {
            List<object> chartData = new List<object>();
            chartData.Add(new object[]
            {
                "Категория", "Сумма"
            });
            foreach(var item in _db.ExpenseCategories.ToList())
            {
                chartData.Add(new object[]
            {
                $"{item.Name}", _db.Expenses.Where(cat => cat.Category.Name == $"{item.Name}" && (cat.dateTime > DateTime.Now.AddDays(-7) && cat.dateTime < DateTime.Now.AddDays(1))).Select(cat => cat.Amount).Sum()
            });
            }
            return Json(chartData);
        }

        //[Route("Expense/Index")]
        [HttpPost]
        public JsonResult GetMonthlyExpense()
        {
            List<object> chartData = new List<object>();
            chartData.Add(new object[]
            {
                "Категория", "Сумма"
            });
            foreach (var item in _db.ExpenseCategories.ToList())
            {
                chartData.Add(new object[]
            {
                $"{item.Name}", _db.Expenses.Where(cat => cat.Category.Name == $"{item.Name}" && (cat.dateTime > DateTime.Now.AddDays(-DateTime.Today.Day))).Select(cat => cat.Amount).Sum()
            });
            }
            return Json(chartData);
        }

        public IActionResult ExpenseReport()
        {
            return View();
        }

        // GET-Create
        public IActionResult Create()
        {
            return View(ExpenseMV());
        }

        // POST-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ExpenseMV obj)
        {
            if (ModelState.IsValid)
            {
                _db.Expenses.Add(obj.Expense);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ExpenseMV());
        }

        // GET-Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Expenses.Find(id);
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
            var obj = _db.Expenses.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Expenses.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET-Update
        public IActionResult Update(int? id)
        {
            ExpenseMV expense = ExpenseMV();
            if (id == null || id == 0)
            {
                return NotFound();
            }
            expense.Expense = _db.Expenses.Find(id);
            if (expense.Expense == null)
            {
                return NotFound();
            }
            return View(expense);
        }

        // POST-Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ExpenseMV obj)
        {
            if (ModelState.IsValid)
            {
                _db.Expenses.Update(obj.Expense);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ExpenseMV());
        }

    }
}
