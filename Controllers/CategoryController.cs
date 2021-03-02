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
            if (ModelState.IsValid) //checks if rules defined in the model is valid see category.cs
            {
                _db.Category.Add(obj); //adds the category obj passed to create function as a record to Category table in database 
                _db.SaveChanges(); //persists the new record to the database

                return RedirectToAction("Index"); 
                /*the redirecttoaction accepts an action method as parameter in this case it is Index, since Index is in this 
                  controller the controller name does not have to be provided*/
            }
            return View(obj); //return error message if rules are invalid

        }

        //GET - EDIT see Views/Category/edit.cshtml
        public IActionResult Edit(int? id) //int? indicates that the field in this case id can be nullable
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _db.Category.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Category.Update(obj);

                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(obj);

        }

        //GET - DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Category.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //POST - DELETE
        [HttpPost]
        //[HttpPost, ActionName("Delete")]
        //[HttpDelete]
        [ValidateAntiForgeryToken]

        /*there are two edit methods one which accepts an int as an argument and another which accepts an obj
         however in order to differenciate the delete methods which both accept an int as an argument a different 
        name is used for 2nd method*/
        /*public IActionResult DeletePost(int? id)
        {
            var obj = _db.Category.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            _db.Category.Remove(obj);

            _db.SaveChanges();

            return RedirectToAction("Index");


        }*/
        public IActionResult Delete(Category obj)
        {

            if (obj == null)
            {
                return NotFound();
            }

            _db.Category.Remove(obj);

            _db.SaveChanges();

            return RedirectToAction("Index");


        }

    }
}
