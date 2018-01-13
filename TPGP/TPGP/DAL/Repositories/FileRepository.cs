using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPGP.Context;
using TPGP.DAL.Interfaces;
using TPGP.Models.Jobs;

namespace TPGP.DAL.Repositories
{
    public class FileRepository : Repository<File>, IFileRepository
    {
        public FileRepository(TPGPContext ctx) : base(ctx)
        {
        }
    }
}