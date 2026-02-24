using Microsoft.AspNetCore.Mvc;
using Jersey.Models;
using Jersey.DataAccess.Data;
using Jersey.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Jersey.Utility;
using Microsoft.AspNetCore.Authorization;

namespace Jersey.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin+","+SD.Role_Manager)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly ICategoryRepository _categoryRepo;

        //Capture the data inside the table : Category
        //private readonly ApplicationDbContext _db;
        //public CategoryController(ApplicationDbContext db) //create the category constructor by inserting the db into it
        //{
        //    _db = db;
        //}

        //public CategoryController(ICategoryRepository db) //use interface db
        //{
        //    _categoryRepo = db;
        //}                     

        public CategoryController(IUnitOfWork unitOfWork) //use IUnitOfWork instead of CategoryRepository
        {
            _unitOfWork = unitOfWork;    
        }

        public IActionResult Index()
        {
            //return the list of Category into the default view Index()
            //List<Category> objCategoryList = _db.Categories.ToList();
            //List<Category> objCategoryList = _categoryRepo.GetAll().ToList();
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(objCategoryList); //return the whole list of category into the view
        }

        public IActionResult Create() 
        {
            //create action and return the corresponding view
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(Category obj) //create the category object and write it into the database
        {
            /*
                If you can have multiple Actions that use the same name, you can use the HttpPost attribute 
                to mark which method gets handled on a Post request like so:             
             */

            //particularly self defined annotation
            if (obj.CatName == obj.DisplayOrder.ToString())
                ModelState.AddModelError("CatName", "Category Name should not be aligned with Display Order!!");

            //Check the form submitted
            if (ModelState.IsValid) 
            {
                //_db.Categories.Add(obj);
                //_db.SaveChanges();
                //_categoryRepo.Add(obj);
                //_categoryRepo.Save();
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = " New League or Team has been added successfully!";
                return RedirectToAction("Index"); // return to the home page , i.e. Index page after obj has been added and saved into the Database
            }
            return View();            
        }

        public IActionResult Edit(int? Id) //The parameter must be Id
        {
            //check the database the id exists or not
            if(Id == null || Id ==0)
                return NotFound();

            // the catid exists
            //Category? categoryFromDb = _db.Categories.Find(Id);
            //Category? categoryFromDb = _categoryRepo.Get(u => u.Id == Id);
            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == Id);

            if (categoryFromDb==null)
                return NotFound();

            return View(categoryFromDb); //return the corresponding view
        }

        [HttpPost]
        public IActionResult Edit(Category obj) //Edit the whole category object and write it into the database
        {
            /*
                If you can have multiple Actions that use the same name, you can use the HttpPost attribute 
                to mark which method gets handled on a Post request like so:             
             */           

            //Check the form submitted
            if (ModelState.IsValid)
            {
                //_db.Categories.Update(obj);
                //_db.SaveChanges();
                //_categoryRepo.Update(obj);
                //_categoryRepo.Save();
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "The Edition of the League or International Team category is successful!!";
                return RedirectToAction("Index"); // return to the home page , i.e. Index page after obj has been updated and saved into the Database
            }
            return View();
        }

        public IActionResult Delete(int? Id) 
        {
            if (Id == null || Id == 0)
                return NotFound();

            // the catid exists
            //Category? categoryFromDb = _db.Categories.Find(Id);
            //Category categoryFromDb = _categoryRepo.Get(u=>u.Id == Id);
            Category categoryFromDb = _unitOfWork.Category.Get(u => u.Id == Id);

            if (categoryFromDb == null)
                return NotFound();

            return View(categoryFromDb); //return the corresponding view
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? Id)
        {
            //Category? categoryFromDb = _db.Categories.Find(Id);
            //Category? obj = _categoryRepo.Get(u=> u.Id == Id);
            Category? obj = _unitOfWork.Category.Get(u => u.Id == Id);

            if (obj == null)
                return NotFound();

            //_db.Categories.Remove(categoryFromDb);
            //_db.SaveChanges();
            //_categoryRepo.Remove(obj);
            //_categoryRepo.Save();
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "The Removal of the League or International Team category is successful!!";
            return RedirectToAction("Index"); //return the corresponding view
        }
    }
}
