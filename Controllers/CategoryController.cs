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
        [HttpPost] //declares that Create() is post action method
        [ValidateAntiForgeryToken]
        /*used to maintain integrity of the form submitted i.e prevent SQL injections and submission of forms from other
          sites to the post url*/
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Category.Add(obj); //adds the category obj passed to create function as a record to Category table in database 
                _db.SaveChanges(); //persists the new record to the database

                return RedirectToAction("Index"); 
                /*the redirecttoaction accepts an action method as parameter in this case it is Index, since Index is in this 
                  controller the controller name does not have to be provided*/
            }
            return View(obj);

        }

    }
}
