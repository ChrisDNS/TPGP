using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TPGP.Context;
using TPGP.DAL.Interfaces;
using TPGP.Models.DAL.Repositories;
using TPGP.Models.Jobs;
using TPGPServices.Models;
using TPGPServices.View_Model;

namespace TPGPServices.Controllers
{
    
    public class ContractsController : ApiController
    {
     
        private readonly IContractRepository contractRepository;
        private readonly IPortfolioRepository portfolioRepository;
     
         public ContractsController(IContractRepository contractRepository, IPortfolioRepository portfolioRepository)
         {
             this.contractRepository = contractRepository;
            this.portfolioRepository = portfolioRepository;
           
         }


       
          public HttpResponseMessage GetAllContracts()
          {
             if (!IsConnected())
             {
                 return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "You must b connected");
            }
             IEnumerable<Contract> contracts = contractRepository.GetAll();
             List<Contract_VM> contracts_vm = new List<Contract_VM>();
            // var jsonSerialiser = new JavaScriptSerializer();
           // List<JObject> contracts_Json = new List<JObject>();
            
            foreach (Contract c in contracts)
              {
               
                 List<string> g = geographicalZonesTOjson(c.Zones.ToList());
               
                contracts_vm.Add(ContractTOjson(c));
              }
            return Request.CreateResponse(HttpStatusCode.OK, contracts_vm);


        }
       
        public HttpResponseMessage GetByZone(string id)
        {
            if (!IsConnected())
            {
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "You must be connected");
            }
            int number;
            if(int.TryParse(id,out number))
            {
                return ContractById(number);
            }
           
            IEnumerable<Contract> contracts = contractRepository.GetAll();
             List<Contract> contracts_zone = new List<Contract>();
          
            foreach ( Contract c in contracts)
            {
               foreach(GeographicalZone g in c.Zones)
                {
                    if (g.Label.Equals(id))
                    {
                        contracts_zone.Add(c);
                    }
                }
             
            }
            List<Contract_VM> contracts_VM = new List<Contract_VM>();
            foreach(Contract c in contracts_zone)
            {
                contracts_VM.Add(ContractTOjson(c));
            }

          
            return Request.CreateResponse(HttpStatusCode.OK, contracts_VM);

        }

        
        public IHttpActionResult PostCreate([FromBody] Contract c)
        {
           
           // string name = o["name"].ToString();
            return Ok("success "+ c.Name);
        }
        //************************ Private Mehode ****************************************
        private HttpResponseMessage ContractById(int id)
        {
            if (!IsConnected())
            {
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "You must be connected");
            }


            Contract c = contractRepository.GetById(id);
            if (c == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Contract doesn't exist");
            }

            return Request.CreateResponse(HttpStatusCode.OK, ContractTOjson(c));

        }


        private Portfolio GetPortfolioById(long id)
        {
            Portfolio p = portfolioRepository.GetById(id);
            return p;
        }

        private Portfolio GetPortfolioBySector(string sector)
        {
            Portfolio p = portfolioRepository.GetByFilter(port => port.Sector.Equals(sector)).First();
            return p;
        }

       
        private List<string> geographicalZonesTOjson(List<GeographicalZone> zones)
        {
            List<string> geo_VM = new List<string>();
            foreach (GeographicalZone g in zones)
            {
                geo_VM.Add(g.Label);
            }
            return geo_VM;
        }

        private Contract_VM ContractTOjson(Contract c)
        {
            List<string> g = geographicalZonesTOjson(c.Zones.ToList());
            Portfolio p = GetPortfolioById(c.PortfolioId);
            Portfolio_VM p_vm = new Portfolio_VM()
            {
                Scope = p.Scope,
                Sector = p.Sector
            };
            Contract_VM c_vm = new Contract_VM()
            {
                Id = c.Id,
                Name = c.Name,
                InitDate = c.InitDate,
                EndDate = c.EndDate,
                Bonus = c.Bonus,
                Company = c.Company,
                Portfolio = p_vm,
                Zones = g

            };
            return c_vm;
        }
        private bool IsConnected()
        {
            if(HttpContext.Current.Session !=null && HttpContext.Current.Session["Username"] != null)
            {
                return true;
            }
            return false;
        }
    }
}

