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
    public class StoreRepository : Repository<Store>, IStoreRepository
    {
        private ApplicationDbContext _db;

        public StoreRepository(ApplicationDbContext db) : base(db) 
        {
            _db= db;
        }
        
        public void Update(Store obj)
        {
            //update the particular store object
            _db.Stores.Update(obj);
        }        
    }
}
