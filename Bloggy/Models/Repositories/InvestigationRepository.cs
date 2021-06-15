using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bloggy.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Bloggy.Models.Repositories
{
    public class InvestigationRepository : IInvestigationRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger _logger;

        public InvestigationRepository(AppDbContext appDbContext, ILogger<InvestigationRepository> logger)
        {
            try
            {
                _appDbContext = appDbContext;
                _logger = logger;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public IEnumerable<Investigation> GetAllInvestigations()
        {
            try
            {
                //Using Eager loading with Include
                return _appDbContext.Investigations.OrderBy(b => b.CreatedDate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public Investigation GetInvestigationById(int investigationId)
        {
            try
            {
                //Using Eager loading with Include
                return _appDbContext.Investigations.Include(b => b.User).FirstOrDefault(p => p.Id == investigationId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public void CreateInvestigation(Investigation investigation)
        {
            try
            {
                _appDbContext.Investigations.Add(investigation);
                _appDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public void DeleteInvestigation(Investigation investigation)
        {
            try
            {
                _appDbContext.Investigations.Remove(investigation);
                _appDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }


        public void UpdateInvestigation(Investigation investigation)
        {
            try
            {
                var existingInvestigation = _appDbContext.Investigations.SingleOrDefault(bp => bp.Id == investigation.Id);
                if (existingInvestigation != null)
                {
                    existingInvestigation.Title = investigation.Title;
                    existingInvestigation.Content = investigation.Content;
                    existingInvestigation.UpdatedDate = investigation.UpdatedDate;
                    existingInvestigation.ImageUrl = investigation.ImageUrl;
                    existingInvestigation.UserId = investigation.UserId;

                    _appDbContext.Entry(existingInvestigation).State = EntityState.Modified;
                    _appDbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
