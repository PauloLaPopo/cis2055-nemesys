using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Bloggy.Models;

namespace Bloggy.ViewModels
{
    public class BlogPostGenreViewModel
    {
        public List<BlogPost> BlogPosts { get; set; }
        public SelectList Categories { get; set; }
        public string BlogPostCategorie { get; set; }
        public string SearchString { get; set; }
    }
}
