using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bloggy.Models.Interfaces;


namespace Bloggy.Models.Repositories
{
    public class MockInvestigationRepository : IInvestigationRepository
    {
        private List<Investigation> _investigations;

        public MockInvestigationRepository()
        {

            if (_investigations == null)
            {
                InitializeInvestigations();
            }

        }


        private void InitializeInvestigations()
        {
            _investigations = new List<Investigation>()
            {
                new Investigation()
                {
                    Id = 1,
                    BlogPostId = 0,
                    Title = "Investigation 1",
                    Content = "The bulb was too old, the administration called the electrician to change the bulb and fix the problem",
                    CreatedDate = DateTime.UtcNow,
                    ImageUrl = "/images/test1.jpg",


                },
                new Investigation()
                {
                    Id = 2,
                    BlogPostId = 0,
                    Title = "Investigation 2",
                    Content = "We are currently analysing the different products used in this experiment. We do not yet know what the problem is",
                    CreatedDate = DateTime.UtcNow.AddDays(-1),
                    ImageUrl = "/images/test2.jpg",
                },
            };
        }

        public IEnumerable<Investigation> GetAllInvestigations()
        {
            List<Investigation> result = new List<Investigation>();
            foreach (var investigation in _investigations)
            {
                result.Add(investigation);
            }
            return result;
        }

        public Investigation GetInvestigationById(int investigationId)
        {
            var investigation = _investigations.FirstOrDefault(p => p.Id == investigationId); //if not found, it returns null

            return investigation;
        }

        public void CreateInvestigation(Investigation investigation)
        {
            investigation.Id = _investigations.Count + 1;
            _investigations.Add(investigation);
        }

        public void DeleteInvestigation(Investigation investigation)
        {
            investigation.Id = _investigations.Count - 1;
            _investigations.Remove(investigation);
        }


        public void UpdateInvestigation(Investigation investigation)
        {
            var existingInvestigation = _investigations.FirstOrDefault(p => p.Id == investigation.Id);
            if (existingInvestigation != null)
            {
                //No need to update CreatedDate (id of course won't be changed)
                existingInvestigation.ImageUrl = investigation.ImageUrl;
                existingInvestigation.Title = investigation.Title;
                existingInvestigation.Content = investigation.Content;
                existingInvestigation.UpdatedDate = investigation.UpdatedDate;
                existingInvestigation.UserId = investigation.UserId;
            }
        }

        public void CreateInvestigation(object investigation)
        {
            throw new NotImplementedException();
        }
    }
}
