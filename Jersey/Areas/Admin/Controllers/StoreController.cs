using Jersey.DataAccess.Data;
using Jersey.DataAccess.Repository.IRepository;
using Jersey.Models;
using Jersey.Models.ViewModels;
using Jersey.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Jersey.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Manager)]
    public class StoreController : Controller
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

        public StoreController(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;            
        }

        public IActionResult Index()
        {
            //return the list of Category into the default view Index()
            //List<Category> objCategoryList = _db.Categories.ToList();
            //List<Category> objCategoryList = _categoryRepo.GetAll().ToList();
            List<Store> objStoreList = _unitOfWork.Store.GetAll().ToList();
            return View(objStoreList); //return the whole list of store into the view
        }

        //public IActionResult Create() 
        //{
        //create pulldown menu for category selection 
        //IEnumerable<SelectListItem> CategoryList =
        //_unitOfWork.Category.GetAll().Select(u => new SelectListItem
        //{
        //    Text = u.CatName,
        //    Value = u.Id.ToString()

        //});

        //    ////Usage of ViewBag
        //    ////ViewBag.CategoryList = CategoryList;

        //    ////Usage of ViewData
        //    //ViewData["CatagoryList"] = CategoryList;

        //    //Use ProductVM instead of product itself
        //    ProductVM productVM = new()
        //    {
        //        CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
        //        {
        //            Text = u.CatName,
        //            Value = u.Id.ToString()  
        //        }),
        //        Product = new Product()
        //    };

        //    //create action and return the corresponding view            
        //    //return View();
        //    return View(productVM);
        //}

        [HttpPost]
        public IActionResult Create(Store obj) //create the category object and write it into the database
        {
            /*
                If you can have multiple Actions that use the same name, you can use the HttpPost attribute 
                to mark which method gets handled on a Post request like so:             
             */


            //Check the form submitted
            if (ModelState.IsValid)
            {
                //_db.Categories.Add(obj);
                //_db.SaveChanges();
                //_categoryRepo.Add(obj);
                //_categoryRepo.Save();
                _unitOfWork.Store.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "The store has been added successfully!";
                return RedirectToAction("Index"); // return to the home page , i.e. Index page after obj has been added and saved into the Database
            }
            return View();
        }

        //[HttpPost]
        //public IActionResult Create(ProductVM obj) 
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.Product.Add(obj.Product);
        //        _unitOfWork.Save();
        //        TempData["success"] = "The product Jersey has been added successfully!";
        //        return RedirectToAction("Index"); // return to the home page , i.e. Index page after obj has been added and saved into the Database
        //    }
        //    else 
        //    {
        //        obj.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
        //        {
        //            Text = u.CatName,
        //            Value = u.Id.ToString()
        //        });
        //        return View(obj);
        //    }
        //}

        public IActionResult Create()
        {
            //create action and return the corresponding view
            return View();
        }

        //public IActionResult Upsert(int? id) 
        //{            
        //    if (id == null || id == 0)
        //        // continue for addition of new store for finding nothing in id
        //        return View(new Store());
        //    else 
        //    {
        //        //continue for editing
        //        Store storeobj = _unitOfWork.Store.Get(u=>u.Id==id);
        //        return View();   //return the corresponding view
        //    }
        //}

        //[HttpPost] //used for creating new product or submitting forms
        //public IActionResult Upsert(Store storeobj) 
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (storeobj.Id == 0) 
        //        {
        //            _unitOfWork.Store.Add(storeobj);
        //            TempData["success"] = "New store has been added successfully!!!";
        //        }                    
        //        else 
        //        {
        //            _unitOfWork.Store.Update(storeobj);
        //            TempData["success"] = "The store information has been updated successfully!!!";
        //        }

        //        _unitOfWork.Save();                                

        //        return RedirectToAction("Index"); //the home/index page of store
        //    }
        //    else
        //    {
        //        //invalid operation
        //        return View(storeobj);
        //    }
        //}

        public IActionResult Edit(int? Id) //The parameter must be Id
        {
            //check the database the id exists or not
            if (Id == null || Id == 0)
                return NotFound();

            Store? storeFromDb = _unitOfWork.Store.Get(u => u.Id == Id);

            if (storeFromDb == null)
                return NotFound();

            return View(storeFromDb); //return the corresponding view
        }

        [HttpPost]
        public IActionResult Edit(Store obj) //Edit the whole category object and write it into the database
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
                _unitOfWork.Store.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "The Edition of the Store is successful!!";
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
            Store storeFromDb = _unitOfWork.Store.Get(u => u.Id == Id);

            if (storeFromDb == null)
                return NotFound();

            return View(storeFromDb); //return the corresponding view
        }

        [HttpPost, ActionName("Delete")] //Specify the actual delete action instead of calling action Delete
        public IActionResult DeletePOST(int? Id)
        {
            //Category? categoryFromDb = _db.Categories.Find(Id);
            //Category? obj = _categoryRepo.Get(u=> u.Id == Id);
            Store? obj = _unitOfWork.Store.Get(u => u.Id == Id);

            if (obj == null)
                return NotFound();

            //_db.Categories.Remove(categoryFromDb);
            //_db.SaveChanges();
            //_categoryRepo.Remove(obj);
            //_categoryRepo.Save();
            _unitOfWork.Store.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "The Removal of the store is successful!!";
            return RedirectToAction("Index"); //return the corresponding view
        }

        //#region API CALLS
        ///* adopting datatable so need to use API calls to fulfill it*/
        //[HttpGet]
        //public IActionResult GetAll() 
        //{
        //    List<Store> objStoreList = _unitOfWork.Store.GetAll().ToList(); //GetAll means get all the stores displayed on one page
        //    return Json(new { data = objStoreList }); //return the store data in JSON format.
        //}

        //[HttpDelete]
        //public IActionResult Delete(int? id)
        //{
        //    var storeToBeDeleted = _unitOfWork.Store.Get(u => u.Id == id);

        //    if(storeToBeDeleted == null)
        //        return Json(new { success=false, message = "Deletion operation is failure!!!" });                       

        //    _unitOfWork.Store.Remove(storeToBeDeleted);
        //    _unitOfWork.Save();
        //    return Json(new { success = true, message = "Deletion operation is successful!!!" });           
        //}        
        //#endregion

    }
}
