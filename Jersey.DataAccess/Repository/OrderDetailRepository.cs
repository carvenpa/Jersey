using Jersey.DataAccess.Data;
using Jersey.DataAccess.Repository.IRepository;
using Jersey.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jersey.DataAccess.Repository
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private ApplicationDbContext _db;
        public OrderDetailRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        //must use save method
        public void Update(OrderDetail obj) 
        {
            _db.OrderDetails.Update(obj);
        }
    }
}
