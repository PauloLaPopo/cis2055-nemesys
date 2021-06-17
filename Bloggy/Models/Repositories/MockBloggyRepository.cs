using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bloggy.Models.Interfaces;

namespace Bloggy.Models.Repositories
{
    //WRAP ALL METHODS IN TRY CATCH BLOCKS
    public class MockBloggyRepository : IBloggyRepository
    {
        private List<BlogPost> _posts;
        private List<Category> _categories;

        public MockBloggyRepository()
        {
            if (_categories == null)
            {
                InitializeCategories();
            }

            if (_posts == null)
            {
                InitializeBlogPosts();
            }

        }
        private void InitializeCategories()
        {
            _categories = new List<Category>()
            {
                new Category()
                {
                    Id = 1,
                    Name = "Condition"
                },
                new Category()
                {
                    Id = 2,
                    Name = "Equipment or Structure"
                }
            };
        }
        

        private void InitializeBlogPosts()
        {
            _posts = new List<BlogPost>()
            {
                new BlogPost()
                {
                    Id = 1,
                    Title = "Toilet light",
                    Content = "the light bulb in the toilet exploded. Many pieces of glass were found on the ground",
                    CreatedDate = DateTime.UtcNow,
                    ImageUrl = "/images/test1.jpg",
                    CategoryId = 1,
                    Status = "Open",
                    UpVotes = 0,
                    Location = "Toilet",

                },
                new BlogPost()
                {
                    Id = 2,
                    Title = "Chimic reaction problem",
                    Content = "Chemical reaction in a laboratory following an experiment during a UoM course. No injuries but material damage was reported",
                    CreatedDate = DateTime.UtcNow.AddDays(-1),
                    ImageUrl = "/images/test2.jpg",
                    CategoryId = 2,
                    Status = "Being Investigated",
                    UpVotes = 0,
                    Location = "Laboratory"
                },
                new BlogPost()
                {
                    Id = 3,
                    Title = "Door of the 122 room",
                    Content = "The door to room 122 was found with the handle broken off. This happened during the night no clue as to how this problem arose.",
                    CreatedDate = DateTime.UtcNow.AddDays(-2),
                    ImageUrl = "/images/test3.jpg",
                    CategoryId = 2,
                    Status = "Being Investigated",
                    UpVotes = 0,
                    Location = "Room 122"
                }
            };
        }

        public IEnumerable<BlogPost> GetAllBlogPosts()
        {
            List<BlogPost> result = new List<BlogPost>();
            foreach (var post in _posts)
            {
                post.Category = _categories.FirstOrDefault(c => c.Id == post.CategoryId);
                result.Add(post);
            }
            return result;
        }

        public BlogPost GetBlogPostById(int blogPostId)
        {
            var post = _posts.FirstOrDefault(p => p.Id == blogPostId); //if not found, it returns null
            var category = _categories.FirstOrDefault(c => c.Id == post.CategoryId);

            post.Category = category;
            return post;
        }

        public void CreateBlogPost(BlogPost blogPost)
        {
            blogPost.Id = _posts.Count + 1;
            _posts.Add(blogPost);
        }

        public void DeleteBlogPost(BlogPost blogPost)
        {
            blogPost.Id = _posts.Count - 1;
            _posts.Remove(blogPost);
        }


        public void UpdateBlogPost(BlogPost blogPost)
        {
            var existingBlogPost = _posts.FirstOrDefault(p => p.Id == blogPost.Id);
            if (existingBlogPost != null)
            {
                //No need to update CreatedDate (id of course won't be changed)
                existingBlogPost.ImageUrl = blogPost.ImageUrl;
                existingBlogPost.Title = blogPost.Title;
                existingBlogPost.Content = blogPost.Content;
                existingBlogPost.UpdatedDate = blogPost.UpdatedDate;
                existingBlogPost.CategoryId = blogPost.CategoryId;
                existingBlogPost.Status = blogPost.Status;
                existingBlogPost.UserId = blogPost.UserId;
                existingBlogPost.Location = blogPost.Location;
            }
        }
        public void UpdateBlogPostReadCount(BlogPost blogPost)
        {
            var existingBlogPost = _posts.FirstOrDefault(p => p.Id == blogPost.Id);
            if (existingBlogPost != null)
            {
                //No need to update CreatedDate (id of course won't be changed)
                existingBlogPost.ReadCount = blogPost.ReadCount + 1;
            }
        }


        public IEnumerable<Category> GetAllCategories()
        {
            return _categories;
        }

        public Category GetCategoryById(int categoryId)
        {
            return _categories.FirstOrDefault(c => c.Id == categoryId); //if not found, it returns null
        }

    }
}
