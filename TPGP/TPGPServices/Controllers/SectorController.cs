using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TPGP.DAL.Interfaces;
using TPGP.Models.Jobs;
using TPGPServices.Models;

namespace TPGPServices.Controllers
{
    public class SectorController : ApiController
    {
        private readonly IContractRepository contractRepository;

        public SectorController(IContractRepository contractRepository)
        {
            this.contractRepository = contractRepository;
        }
        public HttpResponseMessage GetBySector(string id)
        {
            if(!(HttpContext.Current.Session != null && HttpContext.Current.Session["Username"] != null))
            {
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "You must be connected");
            }

            IEnumerable<Contract> contracts = contractRepository.GetByFilter(c => c.Portfolio.Sector.Equals(id));
            List<Contract_VM> contracts_vm = new List<Contract_VM>();
            foreach(Contract c in contracts)
            {
                List<string> geo_VM = new List<string>();
                foreach (GeographicalZone g in c.Zones)
                {
                    geo_VM.Add(g.Label);
                }
                contracts_vm.Add(new Contract_VM()
                {
                    Id =c.Id,
                    Name = c.Name,
                    Bonus = c.Bonus,
                    Company = c.Company,
                    InitDate = c.InitDate.ToString(),
                    EndDate = c.EndDate.ToString(),
                    Sector = c.Portfolio.Sector,
                    Zones = geo_VM
                });
            }
            return Request.CreateResponse(HttpStatusCode.OK, contracts_vm);



        }
    }
}
