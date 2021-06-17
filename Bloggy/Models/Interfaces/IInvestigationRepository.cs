using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bloggy.Models.Interfaces
{
    public interface IInvestigationRepository
    {
        IEnumerable<Investigation> GetAllInvestigations();
        Investigation GetInvestigationById(int investigationId);
        void CreateInvestigation(Investigation newInvestigation);
        void DeleteInvestigation(Investigation investigation);
        void UpdateInvestigation(Investigation updatedInvestigation);
        void CreateInvestigation(object investigation);
    }
}
