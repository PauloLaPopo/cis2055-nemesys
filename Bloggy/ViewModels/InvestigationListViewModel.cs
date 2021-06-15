using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bloggy.ViewModels
{
    public class InvestigationListViewModel
    {
        public int TotalEntries { get; set; }
        public IEnumerable<InvestigationViewModel> Investigations { get; set; }
        public int TotalEntries1 { get; set; }
        public IEnumerable<InvestigationViewModel> Investigations1 { get; set; }
    }
}
