using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TPGP.ActionFilters;
using TPGP.DAL.Interfaces;
using TPGP.Models.Jobs;
using TPGP.Models.ViewModels;

namespace TPGP.Controllers
{
    [CustomAuthorize]
    public class ContractController : Controller
    {
        private readonly IContractRepository contractRepository;
        private readonly IPortfolioRepository portfolioRepository;
        private readonly IGeographicalZoneRepository zoneRepository;
        private readonly IScopeRepository scopeRepository;

        public ContractController(IContractRepository contractRepository,
                                  IPortfolioRepository portfolioRepository,
                                  IGeographicalZoneRepository zoneRepository,
                                  IScopeRepository scopeRepository)
        {
            this.contractRepository = contractRepository;
            this.portfolioRepository = portfolioRepository;
            this.zoneRepository = zoneRepository;
            this.scopeRepository = scopeRepository;
        }

        public ActionResult Index(int? page, string sortOrder, string searchString)
        {
            int noPage = (page ?? 1) - 1;
            var contractsViewModels = new ContractsViewModel
            {

            };

            if (searchString != null)
                page = 1;

            var contracts = contractRepository.Pagination(c => c.Name, noPage, 10, out int total);

            if (contracts.Count() == 0)
            {
                contractsViewModels.IsListEmpty = true;
                return View(contractsViewModels);
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                contracts = contractRepository.GetByFilter(c => c.Name.ToLower().Contains(searchString.ToLower()));

                //var zones = zoneRepository.GetAll().Include("Contracts").Where(z => z.Label.ToLower().Contains(searchString.ToLower()));
                //contracts.ToList().ForEach(c => zones.Where(z => z.Label.Contains(searchString)));
            }

            contractsViewModels.Contracts = new StaticPagedList<Contract>(contracts, noPage + 1, 10, total);

            return View(contractsViewModels);
        }

        public ActionResult Details(long id)
        {
            var cvm = new ContractViewModel
            {
                Contract = contractRepository.GetById(id)
            };

            return View(cvm);
        }

        public ActionResult Create()
        {
            var initialPortfolios = portfolioRepository.GetPortfoliosByUserScope((long)Session["id"]);
            initialPortfolios.ToList().ForEach(p => p.Scope = scopeRepository.GetScopeByPortfolio(p.Id) ? "Initial" : "Extent");
            initialPortfolios = initialPortfolios.Where(p => p.Scope == "Initial");

            var cvm = new ContractViewModel
            {
                Portfolios = new SelectList(initialPortfolios, dataValueField: "Id", dataTextField: "Sector"),
                Zones = zoneRepository.GetAll()
            };

            return View(cvm);
        }

        [HttpPost]
        public ActionResult Create(ContractViewModel cvm)
        {
            if (ModelState.IsValid)
            {
                var zonesFromDbs = zoneRepository.GetByFilter(z => cvm.ZonesIds.Contains(z.Id));

                var selectedZones = new List<GeographicalZone>();
                zonesFromDbs.ToList().ForEach(z => selectedZones.Add(z));

                var userZone = (string)Session["zone"];
                selectedZones.Add(zoneRepository.GetByFilter(z => z.Label == userZone).FirstOrDefault());

                var contract = new Contract
                {
                    Name = cvm.Contract.Name,
                    InitDate = cvm.Contract.InitDate,
                    EndDate = cvm.Contract.EndDate,
                    Bonus = cvm.Contract.Bonus,
                    Company = cvm.Contract.Company,
                    PortfolioId = cvm.Contract.PortfolioId,
                    Zones = selectedZones
                };

                contractRepository.Insert(contract);
                contractRepository.SaveChanges();

                return RedirectToAction("Index", "Portfolio/" + contract.PortfolioId);
            }

            return View();
        }

        public ActionResult Edit(long id)
        {
            var contract = contractRepository.GetById(id);

            var zonesIds = new List<long>();
            contract.Zones.ToList().ForEach(z => zonesIds.Add(z.Id));

            var cvm = new ContractViewModel
            {
                Portfolios = new SelectList(portfolioRepository.GetAll(), dataValueField: "Id", dataTextField: "Sector"),
                Contract = contract,
                Zones = zoneRepository.GetAll(),
                ZonesIds = zonesIds
            };

            return View(cvm);
        }

        [HttpPost]
        public ActionResult Edit(ContractViewModel cvm)
        {
            if (ModelState.IsValid)
            {
                var contract = contractRepository.GetAll().Include("Zones").Where(c => c.Id == cvm.Contract.Id).FirstOrDefault();
                var newZones = zoneRepository.GetAll().Where(z => cvm.ZonesIds.Any(id => id == z.Id));

                contract.Zones.Clear();

                newZones.ToList().ForEach(z => contract.Zones.Add(z));

                contract.Name = cvm.Contract.Name;
                contract.InitDate = cvm.Contract.InitDate;
                contract.EndDate = cvm.Contract.EndDate;
                contract.Bonus = cvm.Contract.Bonus;
                contract.Company = cvm.Contract.Company;
                contract.PortfolioId = cvm.Contract.PortfolioId;

                contractRepository.SaveChanges();

                return RedirectToAction("Index", "Portfolio/" + contract.PortfolioId);
            }

            return View();
        }
    }
}