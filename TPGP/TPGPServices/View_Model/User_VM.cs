using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPGP.Models.Enums;

namespace TPGPServices.View_Model
{
    public class User_VM
    {
        public long Id { get; set; }

       
        public string Username { get; set; }

        public string Firstname { get; set; }

       
        public string Lastname { get; set; }

     
        public string Email { get; set; }

      
        public string Zone { get; set; }

        
        public Roles Role { get; set; }

       

    }
}