using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bloggy.Models
{
    public class Investigation
    {
        public int Id { get; set; }
        public int BlogPostId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }

        //Foreign Key - navigation property name + key property name
        public string UserId { get; set; }
        //Reference navigation property
        public ApplicationUser User { get; set; }

    }
}
