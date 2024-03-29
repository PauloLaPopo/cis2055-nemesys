﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bloggy.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Bloggy.Models.Repositories
{
    public class BloggyRepository : IBloggyRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger _logger;

        public BloggyRepository(AppDbContext appDbContext, ILogger<BloggyRepository> logger)
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

        public IEnumerable<BlogPost> GetAllBlogPosts()
        {
            try
            {
                //Using Eager loading with Include
                return _appDbContext.BlogPosts.Include(b => b.Category).Include(b => b.Status).OrderBy(b => b.CreatedDate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public BlogPost GetBlogPostById(int blogPostId)
        {
            try
            {
                //Using Eager loading with Include
                return _appDbContext.BlogPosts.Include(b => b.Category).Include(b => b.Status).Include(b => b.User).FirstOrDefault(p => p.Id == blogPostId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public void CreateBlogPost(BlogPost blogPost)
        {
            try
            {
                _appDbContext.BlogPosts.Add(blogPost);
                _appDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public void UpdateBlogPost(BlogPost blogPost)
        {
            try
            {
                var existingBlogPost = _appDbContext.BlogPosts.SingleOrDefault(bp => bp.Id == blogPost.Id);
                if (existingBlogPost != null)
                {
                    existingBlogPost.Title = blogPost.Title;
                    existingBlogPost.Content = blogPost.Content;
                    existingBlogPost.UpdatedDate = blogPost.UpdatedDate;
                    existingBlogPost.ImageUrl = blogPost.ImageUrl;
                    existingBlogPost.CategoryId = blogPost.CategoryId;
                    existingBlogPost.UserId = blogPost.UserId;
                    existingBlogPost.Location = blogPost.Location;
                    existingBlogPost.Status = blogPost.Status;

                    _appDbContext.Entry(existingBlogPost).State = EntityState.Modified;
                    _appDbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public IEnumerable<Category> GetAllCategories()
        {
            try
            {
                //Not loading related blog posts
                return _appDbContext.Categories;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public Category GetCategoryById(int categoryId)
        {
            try
            {
                //Not loading related blog posts
                return _appDbContext.Categories.FirstOrDefault(c => c.Id == categoryId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public IEnumerable<Status> GetAllStatus()
        {
            try
            {
                //Not loading related blog posts
                return _appDbContext.Statues;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public Status GetStatusById(int statusId)
        {
            try
            {
                //Not loading related blog posts
                return _appDbContext.Statues.FirstOrDefault(c => c.Id == statusId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }



    }
}
