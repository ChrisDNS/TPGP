using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPGP.Models.Jobs
{
    public class File
    {
        public long Id { get; set; }
        public string FilePath { get; set; }

        public File()
        {

        }

        public File(string filePath)
        {
            FilePath = filePath;
        }

    }
}