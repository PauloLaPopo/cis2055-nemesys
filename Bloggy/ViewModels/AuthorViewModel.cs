﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bloggy.ViewModels
{
    public class AuthorViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public int NbBlogPost { get; set; }
    }
}
