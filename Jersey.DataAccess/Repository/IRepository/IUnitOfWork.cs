using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jersey.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        //get the interace ICategoryRepository
        ICategoryRepository Category {  get; }
        void Save();

        //get the interface IProductRepository
        IProductRepository Product { get; }    
        
        //get the interface IStoreRepository
        IStoreRepository Store {  get; }

        //get the interface IShoppingCartRepository
        IShoppingCartRepository ShoppingCart { get; }

        //get the interface IApplicationUserRepository
        IApplicationUserRepository ApplicationUser { get; }

        //get the interfaces from IOrderHeader and IOrderDetail
        IOrderHeaderRepository OrderHeader { get; }
        IOrderDetailRepository OrderDetail { get; }
    }
}
