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
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

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

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment) //use IUnitOfWork instead of CategoryRepository, use webhostenvironment to host the image file
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            //return the list of Category into the default view Index()
            //List<Category> objCategoryList = _db.Categories.ToList();
            //List<Category> objCategoryList = _categoryRepo.GetAll().ToList();
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties:"Category").ToList();
            return View(objProductList); //return the whole list of product into the view
        }

        //public IActionResult Create() 
        //{
        //    //create pulldown menu for category selection 
        //    //IEnumerable<SelectListItem> CategoryList =
        //    //_unitOfWork.Category.GetAll().Select(u => new SelectListItem
        //    //{
        //    //    Text = u.CatName,
        //    //    Value = u.Id.ToString()

        //    //});

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

        //[HttpPost]
        //public IActionResult Create(Product obj) //create the category object and write it into the database
        //{
        //    /*
        //        If you can have multiple Actions that use the same name, you can use the HttpPost attribute 
        //        to mark which method gets handled on a Post request like so:             
        //     */


        //    //Check the form submitted
        //    if (ModelState.IsValid) 
        //    {
        //        //_db.Categories.Add(obj);
        //        //_db.SaveChanges();
        //        //_categoryRepo.Add(obj);
        //        //_categoryRepo.Save();
        //        _unitOfWork.Product.Add(obj);
        //        _unitOfWork.Save();
        //        TempData["success"] = "The product Jersey has been added successfully!";
        //        return RedirectToAction("Index"); // return to the home page , i.e. Index page after obj has been added and saved into the Database
        //    }
        //    return View();            
        //}

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
        
        public IActionResult Upsert(int? id) 
        {
            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.CatName,
                    Value = u.Id.ToString()
                }),
                Product = new Product()
            };

            if (id == null || id == 0)
                // continue for addition of new product for finding nothing in id
                return View(productVM);
            else 
            {
                //continue for editing
                productVM.Product = _unitOfWork.Product.Get(u=>u.Id==id);
                return View(productVM);
            }
        }

        [HttpPost] //used for creating new product or submitting forms
        public IActionResult Upsert(ProductVM productVM, IFormFile? file) //update and insert with image file submitted
        {
            if (ModelState.IsValid)
            {
                //define the web root path
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                //check the file is input or not
                if (file != null) 
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product"); //combine the rootpath and images\product
                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl)) 
                    {
                        var oldImagePath = Path.Combine(wwwRootPath,productVM.Product.ImageUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(oldImagePath))
                            System.IO.File.Delete(oldImagePath);
                    }
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create)) 
                    {
                        file.CopyTo(fileStream);  //provide the path and create the file and copy the uploaded file into the path
                    }

                    //link to productVM
                    productVM.Product.ImageUrl = @"\images\product\" + fileName;
                }

                if (productVM.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(productVM.Product);
                    _unitOfWork.Save();
                    TempData["success"] = "The product Jersey has been added successfully!";
                }
                else 
                {
                    _unitOfWork.Product.Update(productVM.Product);
                    _unitOfWork.Save();
                    TempData["success"] = "The product Jersey has been updated successfully!";
                }

                return RedirectToAction("Index"); // return to the home page , i.e. Index page after obj has been added and saved into the Database
            }
            else
            {
                productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.CatName,
                    Value = u.Id.ToString()
                });
                return View(productVM);
            }
        }

        //public IActionResult Edit(int? Id) //The parameter must be Id
        //{
        //    //check the database the id exists or not
        //    if(Id == null || Id ==0)
        //        return NotFound();

        //    // the catid exists
        //    //Category? categoryFromDb = _db.Categories.Find(Id);
        //    //Category? categoryFromDb = _categoryRepo.Get(u => u.Id == Id);
        //    Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == Id);

        //    if (productFromDb==null)
        //        return NotFound();

        //    return View(productFromDb); //return the corresponding view
        //}

        //[HttpPost]
        //public IActionResult Edit(Product obj) //Edit the whole category object and write it into the database
        //{
        //    /*
        //        If you can have multiple Actions that use the same name, you can use the HttpPost attribute 
        //        to mark which method gets handled on a Post request like so:             
        //     */           

        //    //Check the form submitted
        //    if (ModelState.IsValid)
        //    {
        //        //_db.Categories.Update(obj);
        //        //_db.SaveChanges();
        //        //_categoryRepo.Update(obj);
        //        //_categoryRepo.Save();
        //        _unitOfWork.Product.Update(obj);
        //        _unitOfWork.Save();
        //        TempData["success"] = "The Edition of the product Jersey is successful!!";
        //        return RedirectToAction("Index"); // return to the home page , i.e. Index page after obj has been updated and saved into the Database
        //    }
        //    return View();
        //}

        //public IActionResult Delete(int? Id) 
        //{
        //    if (Id == null || Id == 0)
        //        return NotFound();

        //    // the catid exists
        //    //Category? categoryFromDb = _db.Categories.Find(Id);
        //    //Category categoryFromDb = _categoryRepo.Get(u=>u.Id == Id);
        //    Product productFromDb = _unitOfWork.Product.Get(u => u.Id == Id);

        //    if (productFromDb == null)
        //        return NotFound();

        //    return View(productFromDb); //return the corresponding view
        //}

        //[HttpPost, ActionName("Delete")] //Specify the actual delete action instead of calling action Delete
        //public IActionResult DeletePOST(int? Id)
        //{
        //    //Category? categoryFromDb = _db.Categories.Find(Id);
        //    //Category? obj = _categoryRepo.Get(u=> u.Id == Id);
        //    Product? obj = _unitOfWork.Product.Get(u => u.Id == Id);

        //    if (obj == null)
        //        return NotFound();

        //    //_db.Categories.Remove(categoryFromDb);
        //    //_db.SaveChanges();
        //    //_categoryRepo.Remove(obj);
        //    //_categoryRepo.Save();
        //    _unitOfWork.Product.Remove(obj);
        //    _unitOfWork.Save();
        //    TempData["success"] = "The Removal of the product Jersey is successful!!";
        //    return RedirectToAction("Index"); //return the corresponding view
        //}

        #region API CALLS
        /* adopting datatable so need to use API calls to fulfill it*/
        [HttpGet]
        public IActionResult GetAll() 
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties:"Category").ToList();
            return Json(new { data = objProductList }); //return the product data in JSON format.
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productToBeDeleted = _unitOfWork.Product.Get(u => u.Id == id);

            if(productToBeDeleted == null)
                return Json(new { success=false, message="Deletion operation is failure!!!" });

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, productToBeDeleted.ImageUrl.TrimStart('\\'));
            
            if (System.IO.File.Exists(oldImagePath))
                System.IO.File.Delete(oldImagePath);
            
            _unitOfWork.Product.Remove(productToBeDeleted);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deletion operation is successful!!!" });           
        }
        
        #endregion

    }
}
