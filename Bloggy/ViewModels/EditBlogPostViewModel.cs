using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Bloggy.ViewModels
{
    public class EditBlogPostViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "A title is required")]
        [StringLength(50)]
        [Display(Name = "Blog heading")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Blog post content is required")]
        [StringLength(1500, ErrorMessage = "Blog post cannot be longer than 1500 characters")]
        public string Content { get; set; }
        [Required(ErrorMessage = "A location is required")]
        [StringLength(50)]
        [Display(Name = "Location")]
        public string Location { get; set; }

        [Display(Name = "The Display Name")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yy H:mm:ss tt}")]
        [DataType(DataType.DateTime)]
        public DateTime DiscoveryDate { get; set; }
        public string ImageUrl { get; set; }
        [Display(Name = "Featured Image")]
        public IFormFile ImageToUpload { get; set; } //used only when submitting form
        [Display(Name = "Blog Post Category")]

        //Property used to bind user selection
        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }
        public string Status { get;set; }

        //Property used solely to populate drop down
        public List<CategoryViewModel> CategoryList { get; set; }
    }
}
