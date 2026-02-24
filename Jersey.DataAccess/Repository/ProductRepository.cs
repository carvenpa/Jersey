using Jersey.DataAccess.Data;
using Jersey.DataAccess.Repository.IRepository;
using Jersey.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Jersey.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db) 
        {
            _db= db;
        }                                 

        public void Update(Product obj)
        {
            //update the whole category
            //_db.Products.Update(obj);

            var objFromDb = _db.Products.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null) 
            {
               objFromDb.CategoryId = obj.CategoryId;
               objFromDb.Description = obj.Description;
               objFromDb.Edition = obj.Edition;
               objFromDb.Price = obj.Price;
               objFromDb.ProductName = obj.ProductName;
               objFromDb.Season = obj.Season;
               objFromDb.Size = obj.Size;
               objFromDb.SleeveType = obj.SleeveType;
               if (objFromDb.ImageUrl != null)
               {
                    objFromDb.ImageUrl = obj.ImageUrl;     
               }
               objFromDb.Printing = obj.Printing;
            }                
        }
    }
}
