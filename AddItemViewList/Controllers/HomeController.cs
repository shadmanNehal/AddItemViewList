using AddItemViewList.Data;
using AddItemViewList.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AddItemViewList.Controllers
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

        public IActionResult AddList()
        {
            return View( _db.GetList());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult AddItems(AddList addList)
        {
            if(addList.ItemId != 0)
            {
                _db.editItemSP(addList.ItemId, addList.ItemName, addList.ItemType, addList.ItemPrice);
                return Json(new {isUpdate = true, success = true, message = "Edited Successfully", data = addList });
            }
            
            _db.execAddItemSP(addList.ItemName,addList.ItemType,addList.ItemPrice,out int itemId);
            addList.ItemId = itemId;
            return Json(new { isUpdate = false, success = true, message = "Operation Delete/Edit Successful",data= addList });
            
        }

        [HttpGet]
        public IActionResult getItemList(Filter filter)
        {         
            List<AddList> list = _db.getPagin(filter.pageNo, filter.pageSize,out int loCnt);
            DataCountM dataCount = new DataCountM() { 
                AddLists = list,
                dataCount = loCnt
            };
            return View("~/Views/Home/_ItemList.cshtml", dataCount);
        }
        
        
    }
}
