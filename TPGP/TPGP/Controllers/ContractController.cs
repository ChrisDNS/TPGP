using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TPGP.ActionFilters;
using TPGP.Context;
using TPGP.DAL.Interfaces;
using TPGP.Models.Jobs;
using TPGP.Models.ViewModels;
using TPGP.ViewModels;

namespace TPGP.Controllers
{
    [CustomAuthorize]
    public class ContractController : Controller
    {
        private readonly IContractRepository contractRepository;
        private readonly IPortfolioRepository portfolioRepository;

        public ContractController(IContractRepository contractRepository,
                                  IPortfolioRepository portfolioRepository)
        {
            this.contractRepository = contractRepository;
            this.portfolioRepository = portfolioRepository;
        }

        public ActionResult Index(string currentFilter, string searchString)
        {
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
                Zones = CreateData()               
            };

            return View(cvm);
        }

        private IEnumerable<AssignedGeographicalZoneData> CreateData()
        {
            var ctx = new TPGPContext();
            var zones = ctx.Zones;
            var assignedZones = new List<AssignedGeographicalZoneData>();

            foreach(var item in zones)
            {
                assignedZones.Add(new AssignedGeographicalZoneData
                {
                    Id = item.Id,
                    Label = item.Label,
                    Assigned = false
                });
            }

            return assignedZones;
        }
 
        [HttpPost]
        public ActionResult Save(ContractViewModel cvm)
        {
            if (ModelState.IsValid)
            {
                var contract = new Contract
                {
                    Name = cvm.Contract.Name,
                    InitDate = cvm.Contract.InitDate,
                    Bonus = cvm.Contract.Bonus,
                    PortfolioId = cvm.Contract.PortfolioId
                };

                contractRepository.Insert(contract);
                contractRepository.SaveChanges();

                return RedirectToAction("Index", "Portfolio");
            }

            return View();
        }
    }
}