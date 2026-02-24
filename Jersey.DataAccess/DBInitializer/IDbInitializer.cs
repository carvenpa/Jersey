using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jersey.Models;
using Jersey.Utility;
using Jersey.DataAccess.Data;

namespace Jersey.DataAccess.DBInitializer
{
    public interface IDbInitializer
    {
        //The interface IDbInitializer serves as the public connector. As we want to establish the database initlializer so we setup the method Initialize
        //for the class DbInitializer to consume
        void Initialize();
    }
}
