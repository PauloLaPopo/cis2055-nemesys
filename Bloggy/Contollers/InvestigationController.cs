using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bloggy.Models;
using Bloggy.Models.Interfaces;
using Bloggy.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Bloggy.Contollers
{
    public class InvestigationController : Controller
    {
        private readonly IBloggyRepository _bloggyRepository;
        private readonly IInvestigationRepository _investigationRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<InvestigationController> _logger;

        public InvestigationController(
            IBloggyRepository bloggyRepository,
            IInvestigationRepository investigationRepository,
            UserManager<ApplicationUser> userManager,
            ILogger<InvestigationController> logger)
        {
            _bloggyRepository = bloggyRepository;
            _investigationRepository = investigationRepository;
            _userManager = userManager;
            _logger = logger;
        }
        public IActionResult Index()
        {
            try
            {
                var model = new InvestigationListViewModel()
                {
                    TotalEntries = _investigationRepository.GetAllInvestigations().Count(),
                    Investigations = _investigationRepository
                    .GetAllInvestigations()
                    .OrderByDescending(b => b.CreatedDate)
                    .Select(b => new InvestigationViewModel()
                    {
                        Id = b.Id,
                        CreatedDate = b.CreatedDate,
                        Content = b.Content,
                        ImageUrl = b.ImageUrl,
                        Title = b.Title,

                        Author = new AuthorViewModel()
                        {
                            Id = b.UserId,
                            Name = (_userManager.FindByIdAsync(b.UserId).Result != null) ? _userManager.FindByIdAsync(b.UserId).Result.UserName : "Anonymous",
                            UserName = (_userManager.FindByIdAsync(b.UserId).Result != null) ? _userManager.FindByIdAsync(b.UserId).Result.AuthorAlias : "Anonymous"
                        }
                    })
                };
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, ex.Data);
                return View("Error");
            }
        }

        public IActionResult Details(int id)
        {
            try
            {
                var investigation = _investigationRepository.GetInvestigationById(id);
                if (investigation == null)
                    return NotFound();
                else
                {
                    var model = new InvestigationViewModel()
                    {
                        Id = investigation.Id,
                        CreatedDate = investigation.CreatedDate,
                        ImageUrl = investigation.ImageUrl,
                        Title = investigation.Title,
                        Content = investigation.Content,
                        Author = new AuthorViewModel()
                        {
                            Id = investigation.UserId,
                            Name = (_userManager.FindByIdAsync(investigation.UserId).Result != null) ? _userManager.FindByIdAsync(investigation.UserId).Result.UserName : "Anonymous"
                        }
                    };

                    return View(model);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, ex.Data);
                return View("Error");
            }

        }


        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create(int id)
        {
            try
            {
                    var model = new EditInvestigationViewModel();
                    //Pass model to View
                    return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, ex.Data);
                return View("Error");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create([FromRoute] int id, [Bind("Title, Content, ImageToUpload")] EditInvestigationViewModel newInvestigation)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    /*
                    string fileName = "";
                    if (newInvestigation.ImageToUpload != null)
                    {
                        //At this point you should check size, extension etc...
                        //Then persist using a new name for consistency (e.g. new Guid)
                        var extension = "." + newInvestigation.ImageToUpload.FileName.Split('.')[newInvestigation.ImageToUpload.FileName.Split('.').Length - 1];
                        fileName = Guid.NewGuid().ToString() + extension;
                        var path = Directory.GetCurrentDirectory() + "\\wwwroot\\images\\investigations\\" + fileName;
                        using (var bits = new FileStream(path, FileMode.Create))
                        {
                            newInvestigation.ImageToUpload.CopyTo(bits);
                        }
                    }
                    */
                    var existingBlogPost = _bloggyRepository.GetBlogPostById(id);

                    Investigation investigation = new Investigation()
                    {
                        BlogPostId = id,
                        Title = existingBlogPost.Title,
                        Content = newInvestigation.Content,
                        CreatedDate = DateTime.UtcNow,
                        ImageUrl = existingBlogPost.ImageUrl,
                        UserId = _userManager.GetUserId(User)
                    };

                    _investigationRepository.CreateInvestigation(investigation);
                    existingBlogPost.Status = "Being Investigated";
                    _bloggyRepository.UpdateBlogPost(existingBlogPost);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(newInvestigation);
                }
            }
            catch (DbUpdateException e)
            {

                return View(newInvestigation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, ex.Data);
                return View("Error");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(int id)
        {
            var existingInvestigation = _investigationRepository.GetInvestigationById(id);
            if (existingInvestigation != null)
            {
                EditInvestigationViewModel model = new EditInvestigationViewModel()
                {
                    Id = existingInvestigation.Id,
                    BlogPostId = existingInvestigation.BlogPostId,
                    Title = existingInvestigation.Title,
                    Content = existingInvestigation.Content,
                    ImageUrl = existingInvestigation.ImageUrl,
                };

                return View(model);
            }
            else
                return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrator")]

        public IActionResult DeleteConfirmed(int id)
        {
            var modelToDelete = _investigationRepository.GetInvestigationById(id);
            if (modelToDelete == null)
            {
                return NotFound();
            }
            int ID = modelToDelete.BlogPostId;
            var investigatedBlogPost = _bloggyRepository.GetBlogPostById(ID);
            investigatedBlogPost.Status = "Open";
            _bloggyRepository.UpdateBlogPost(investigatedBlogPost);
            _investigationRepository.DeleteInvestigation(modelToDelete);
            return RedirectToAction("Index");
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var existingInvestigation = _investigationRepository.GetInvestigationById(id);
                if (existingInvestigation != null)
                {
                    //Check if the current user has access to this resource
                    var currentUser = await _userManager.GetUserAsync(User);
                    if (existingInvestigation.User.Id == currentUser.Id)
                    {
                        EditInvestigationViewModel model = new EditInvestigationViewModel()
                        {
                            Id = existingInvestigation.Id,
                            Title = existingInvestigation.Title,
                            Content = existingInvestigation.Content,
                            ImageUrl = existingInvestigation.ImageUrl,
                        };

                        return View(model);
                    }
                    else
                        return Unauthorized();
                }
                else
                    return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, ex.Data);
                return View("Error");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, [Bind("Id, Title, Content, ImageToUpload")] EditInvestigationViewModel updatedInvestigation)
        {
            try
            {
                var modelToUpdate = _investigationRepository.GetInvestigationById(id);
                if (modelToUpdate == null)
                {
                    return NotFound();
                }

                //Check if the current user has access to this resource
                var currentUser = await _userManager.GetUserAsync(User);
                if (modelToUpdate.User.Id == currentUser.Id)
                {
                    if (ModelState.IsValid)
                    {

                        string imageUrl = "";

                        if (updatedInvestigation.ImageToUpload != null)
                        {
                            string fileName = "";
                            //At this point you should check size, extension etc...
                            //Then persist using a new name for consistency (e.g. new Guid)
                            var extension = "." + updatedInvestigation.ImageToUpload.FileName.Split('.')[updatedInvestigation.ImageToUpload.FileName.Split('.').Length - 1];
                            fileName = Guid.NewGuid().ToString() + extension;
                            var path = Directory.GetCurrentDirectory() + "\\wwwroot\\images\\investigations\\" + fileName;
                            using (var bits = new FileStream(path, FileMode.Create))
                            {
                                updatedInvestigation.ImageToUpload.CopyTo(bits);
                            }
                            imageUrl = "/images/investigations/" + fileName;
                        }
                        else
                            imageUrl = modelToUpdate.ImageUrl;

                        modelToUpdate.Title = updatedInvestigation.Title;
                        modelToUpdate.Content = updatedInvestigation.Content;
                        modelToUpdate.ImageUrl = imageUrl;
                        modelToUpdate.UpdatedDate = DateTime.Now;
                        modelToUpdate.UserId = _userManager.GetUserId(User);

                        _investigationRepository.UpdateInvestigation(modelToUpdate);

                        return RedirectToAction("Index");
                    }
                    else
                        return Unauthorized(); //or redirect to error controller with 401/403 actions
                }
                else
                {
                    return View(updatedInvestigation);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, ex.Data);
                return View("Error");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Dashboard()
        {
            try
            {
                ViewBag.Title = "Bloggy Dashboard";

                var model = new BlogDashboardViewModel();
                model.TotalRegisteredUsers = _userManager.Users.Count();
                model.TotalEntries = _bloggyRepository.GetAllBlogPosts().Count();

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, ex.Data);
                return View("Error");
            }
        }
    }
}
