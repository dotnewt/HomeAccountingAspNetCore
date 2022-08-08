using HomeAccountingWithAspNet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HomeAccountingWithAspNet.Data;

namespace HomeAccountingWithAspNet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        [Route("Home/Privacy")]
        [HttpPost]
        public JsonResult GetJsonResult()
        {
            List<object> charData = new List<object>();
            charData.Add(new object[]
            {
                "Тип", "Сумма"
            });
            charData.Add(new object[]
            {
                "Доход", _db.Incomes.Where(item => item.dateTime > DateTime.Now.AddDays(-DateTime.Today.Day)).Select(item => item.Amount).Sum()
            });
            charData.Add(new object[]
            {
                "Затраты", _db.Expenses.Where(item => item.dateTime > DateTime.Now.AddDays(-DateTime.Today.Day)).Select(item => item.Amount).Sum()
            });
            return Json(charData);
        }

        public IActionResult Privacy()
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
