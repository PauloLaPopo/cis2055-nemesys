﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bloggy.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bloggy.ViewModels
{
    public class BlogPostListViewModel
    {
        public int TotalEntries { get; set; }
        public IEnumerable<BlogPostViewModel> BlogPosts { get; set; }

        public List<BlogPost> BlogPost { get; set; }
        public SelectList Categories { get; set; }
        public string BlogPostCategorie { get; set; }
    }

}
