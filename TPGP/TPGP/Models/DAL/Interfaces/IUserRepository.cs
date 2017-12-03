using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPGP.Models.Jobs;

namespace TPGP.Models.DAL.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        //fonction specifiques aux Users
    }
}