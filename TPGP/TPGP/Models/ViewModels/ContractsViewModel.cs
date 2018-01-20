using PagedList;
using TPGP.Models.Jobs;

namespace TPGP.Models.ViewModels
{
    public class ContractsViewModel
    {
        public IPagedList<Contract> Contracts { get; set; }

        public string Portfolio { get; set; }

        public string CurrentSort { get; internal set; }
        public string CurrentFilter { get; internal set; }
       

        public bool IsListEmpty;

        public ContractsViewModel()
        {
        }
    }
}