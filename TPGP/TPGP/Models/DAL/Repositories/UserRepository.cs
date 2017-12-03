﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPGP.Models.DAL.Context;
using TPGP.Models.DAL.Interfaces;
using TPGP.Models.Jobs;

namespace TPGP.Models.DAL.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(TPGPContext ctx) : base(ctx)
        {
        }
    }
}