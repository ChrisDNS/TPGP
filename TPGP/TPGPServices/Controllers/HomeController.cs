using LDAP;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TPGP.DAL.Interfaces;
using TPGP.Models.Enums;
using TPGP.Models.Jobs;
using TPGPServices.View_Model;

namespace TPGPServices.Controllers
{
    public class HomeController : ApiController
    {
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;
        public HomeController(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;

        }
          public HttpResponseMessage Post(JObject o)
          {
           
             string username = o["username"].ToString();
             string password = o["password"].ToString();
            
              LDAPUser ldapUserDetails = LDAPService.Instance.AuthenticationAndIdentification(username, password);
              if (ldapUserDetails == null)
              {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Uknowing username or password"); 
              }
             
             var user = userRepository.GetByFilter(u => u.Username == username).FirstOrDefault();
            if (user == null)
            {
                 var newUser = new User
                 {
                     Firstname = ldapUserDetails.Firstname,
                     Lastname = ldapUserDetails.Lastname,
                     Username = ldapUserDetails.Username,
                     Email = ldapUserDetails.Email,
                     //Zone = ldapUserDetails.Zone,
                     Role = roleRepository.GetByFilter(r => r.RoleName == Roles.COLLABORATOR).FirstOrDefault()
                 };
                 user = newUser;
                 userRepository.Insert(newUser);
                 userRepository.SaveChanges();
               
            }
               
           HttpContext.Current.Session["Username"] = username;
            JObject us = new JObject
            {
                { "username", ldapUserDetails.Username },
                { "firstanme", ldapUserDetails.Firstname },
                { "lastname", ldapUserDetails.Lastname },
                { "email", ldapUserDetails.Email },
                { "adress", ldapUserDetails.Address },
                { "zone", ldapUserDetails.Zone },
                { "role", user.Role.RoleName.ToString() }
            };
            return Request.CreateResponse(HttpStatusCode.OK, us); 

         
          }
     
       public HttpResponseMessage Get()
        {
           if( HttpContext.Current.Session["Username"] == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.MethodNotAllowed, "Already disconnect");

            }
            else
            {
                HttpContext.Current.Session["Username"] = null;
                return Request.CreateResponse(HttpStatusCode.OK, "Disconnection success");
            }
        }
    }
}