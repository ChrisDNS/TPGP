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

        public ContractController(IContractRepository contractRepository,
                                  IPortfolioRepository portfolioRepository,
                                  IGeographicalZoneRepository zoneRepository)
        {
            this.contractRepository = contractRepository;
            this.portfolioRepository = portfolioRepository;
            this.zoneRepository = zoneRepository;
        }

        public ActionResult Index(long id, int? page, string sortOrder, string searchString)
        {
            //TODO

            return View();
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
            var cvm = new ContractViewModel
            {
                Portfolios = new SelectList(portfolioRepository.GetAll(), dataValueField: "Id", dataTextField: "Sector"),
                Zones = CreateZoneData()
            };

            return View(cvm);
        }

        private IEnumerable<AssignedGeographicalZoneData> CreateZoneData()
        {
            var zones = zoneRepository.GetAll().ToList();
            var assignedZones = new List<AssignedGeographicalZoneData>();

            zones.ForEach(z => assignedZones.Add(new AssignedGeographicalZoneData
            {
                Id = z.Id,
                Label = z.Label,
                Assigned = false
            }));

            return assignedZones;
        }

        [HttpPost]
        public ActionResult Save(ContractViewModel cvm)
        {
            if (ModelState.IsValid)
            {
                var selectedZones = cvm.Zones.Where(z => cvm.ZonesIds.Contains(z.Id)).ToList();
                var zonesIds = selectedZones.Select(z => z.Id);
                var zonesFromDbs = zoneRepository.GetByFilter(z => zonesIds.Contains(z.Id));

                var listZones = new List<GeographicalZone>();
                zonesFromDbs.ToList().ForEach(z => listZones.Add(z));

                var contract = new Contract
                {
                    Name = cvm.Contract.Name,
                    InitDate = cvm.Contract.InitDate,
                    EndDate = cvm.Contract.EndDate,
                    Bonus = cvm.Contract.Bonus,
                    PortfolioId = cvm.Contract.PortfolioId,
                    Zones = listZones
                };

                contractRepository.Insert(contract);
                contractRepository.SaveChanges();

                return RedirectToAction("Index", "Portfolio/" + contract.PortfolioId);
            }

            return View();
        }
    }
}