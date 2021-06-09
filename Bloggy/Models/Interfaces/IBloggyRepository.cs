using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bloggy.Models.Interfaces
{
    public interface IBloggyRepository
    {
        IEnumerable<BlogPost> GetAllBlogPosts();
        BlogPost GetBlogPostById(int blogPostId);
        void CreateBlogPost(BlogPost newBlogPost);

        void DeleteBlogPost(BlogPost blogPost);

        void UpdateBlogPost(BlogPost updatedBlogPost);

        IEnumerable<Category> GetAllCategories();
        Category GetCategoryById(int categoryId);
        IEnumerable<Status> GetAllStatus();
        Status GetStatusById(int statusId);

    }

}
