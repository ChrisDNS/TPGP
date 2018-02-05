using LDAP;
using System;
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
        private readonly IGeographicalZoneRepository geographicalZoneRepository;
     
         public ContractsController(IContractRepository contractRepository, IPortfolioRepository portfolioRepository,
             IGeographicalZoneRepository geographicalZoneRepository)
         {
            this.contractRepository = contractRepository;
            this.portfolioRepository = portfolioRepository;
            this.geographicalZoneRepository = geographicalZoneRepository;
           
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


        public HttpResponseMessage PostCreate([FromBody] Contract_VM c)
        {
            if (!IsConnected())
               {
                  return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "You must be connected");
             }
            long portfolioId = portfolioRepository.GetByFilter(p => p.Sector.Equals(c.Sector)).First().Id;
            //string zone = LDAPService.Instance.GetLDAPUser(HttpContext.Current.Session["Username"].ToString()).Zone;
           /* string zone = "France";
           
            var zonesFromDbs = geographicalZoneRepository.GetByFilter(z => z.Id.Equals(1));

            var selectedZones = new List<GeographicalZone>();
            zonesFromDbs.ToList().ForEach(z => selectedZones.Add(z));*/
            Contract contract = new Contract
            {
                Name = c.Name,
                InitDate = Convert.ToDateTime(c.InitDate),
                EndDate = Convert.ToDateTime(c.EndDate),
                Bonus = c.Bonus,
                Company = c.Company,
                PortfolioId = portfolioId,
                //Zones = selectedZones
            };         
            contractRepository.Insert(contract);
            contractRepository.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.Created, "Contract successfully created");
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
            Portfolio p = portfolioRepository.GetById(c.PortfolioId);
           
            Contract_VM c_vm = new Contract_VM()
            {
                Id = c.Id,
                Name = c.Name,
                InitDate = c.InitDate.ToString("dd/MM/yyyy"),
                EndDate = c.EndDate.ToString("dd/MM/yyyy"),
                Bonus = c.Bonus,
                Company = c.Company,
                Sector = p.Sector,
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

