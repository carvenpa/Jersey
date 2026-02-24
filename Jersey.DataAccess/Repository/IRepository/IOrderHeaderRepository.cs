using Jersey.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jersey.DataAccess.Repository.IRepository
{
    public interface IOrderHeaderRepository: IRepository<OrderHeader>
    {
        //define only one method for order header to implement
        void Update(OrderHeader obj);

        //update the order status as well
        void UpdateStatus(int id, string orderStatus, string? paymentStatus = null);
    }
}
