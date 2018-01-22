using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TPGP.Context;
using TPGP.DAL.Interfaces;
using TPGP.Models.DAL.Repositories;
using TPGP.Models.Jobs;
using TPGPServices.Models;

namespace TPGPServices.Controllers
{
    public class ContractsController : ApiController
    {
     
       // private IContractRepository c = new ContractRepository(new TPGPContext());
           
        private readonly IContractRepository contractRepository;
        private readonly IPortfolioRepository portfolioRepository;
         public ContractsController(IContractRepository contractRepository, IPortfolioRepository portfolioRepository)
         {
             this.contractRepository = contractRepository;
            this.portfolioRepository = portfolioRepository;
         }

          public List<Contracts_VM> GetAllContracts()
          {

            IEnumerable<Contract> contracts = contractRepository.GetAll();
            var contracts_vm = new List<Contracts_VM>();
            foreach(Contract c in contracts)
            {
               
                Portfolio p = GetPortfolioById(c.PortfolioId);
                contracts_vm.Add(new Contracts_VM()
                {
                    IDContract = c.Id,
                    Name = c.Name,
                    InitDate = c.InitDate,
                    EndDate = c.EndDate,
                    Bonus = c.Bonus,
                    Company = c.Company,
                    Sector= p.Sector
                });
            }
            return contracts_vm;
          }

        public IHttpActionResult GetContractById(int id)
        {
            Contract c =contractRepository.GetById(id);
            if(c == null)
            {
                return NotFound();
            }
            Portfolio p = GetPortfolioById(c.PortfolioId);
            Contracts_VM c_vm = new Contracts_VM()
            {
                IDContract = c.Id,
                Name = c.Name,
                InitDate = c.InitDate,
                EndDate = c.EndDate,
                Bonus = c.Bonus,
                Company = c.Company,
                Sector = p.Sector

            };
            return Ok(c_vm);
        }

        
        public IHttpActionResult Post(string name)
        {
           /* if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }
            contractRepository.Insert(new Contract()
            {
                Name = c_vm.Name,
                InitDate = c_vm.InitDate,
                EndDate = c_vm.EndDate,
                Bonus = c_vm.Bonus,
                Company = c_vm.Company,
                Portfolio = GetPortfolioBySector(c_vm.Sector)
                

            });
            contractRepository.SaveChanges();*/
            return Ok("success");
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


    }
}

