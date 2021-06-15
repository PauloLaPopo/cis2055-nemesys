using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Bloggy.ViewModels
{
    public class EditInvestigationViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "A title is required")]
        [StringLength(50)]
        [Display(Name = "Investigation heading")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Investigation content is required")]
        [StringLength(1500, ErrorMessage = "Investigation cannot be longer than 1500 characters")]
        public string Content { get; set; }

        [Display(Name = "The Display Name")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yy H:mm:ss tt}")]
        [DataType(DataType.DateTime)]
        public DateTime UpdatedDate { get; set; }
        public string ImageUrl { get; set; }
        [Display(Name = "Featured Image")]
        public IFormFile ImageToUpload { get; set; } //used only when submitting form

    }
}
