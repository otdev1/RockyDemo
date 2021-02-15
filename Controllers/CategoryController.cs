using Microsoft.AspNetCore.Mvc;
using RockyDemo.Data;
using RockyDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockyDemo.Controllers
{
    public class CategoryController : Controller
    {
        /*an object(in this case _db) of ApplicationDbContext is needed in order to enable interection between the db and the Rocky application
         therefore line below is used to create an empty object of type ApplicationDbContext*/
        private readonly ApplicationDbContext _db;

        /*to add a corresponding view for a controller function right click on the function and select Add view*/

        /*the categorycontroller function is used to populate the _db object
         since an ApplicationDbContext object has already been added to and configured in the service container i.e 
         the ConfigureServices method (see startup.cs) it can be can be called in the contructor method 
        (which is CategoryController in this case) and its contents used to populate the empty _db object*/
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objList = _db.Category; 
            //retrieves all records i.e categories in the category table and stores them in an object list

            return View(objList); 
            //enables contents of object list ot be accessible in the Index view(see Views/Category/Index.chtml
        }

        //GET - CREATE see Views/Category/Create.cshtml
        public IActionResult Create()
        {
            return View();
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Category.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);

        }

    }
}
