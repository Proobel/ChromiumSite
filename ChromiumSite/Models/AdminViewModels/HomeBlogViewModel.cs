using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChromiumSite.Models.AdminViewModels
{
    public class HomeBlogViewModel
    {
        [StringLength(200, ErrorMessage = "The {0} must be at max {1} characters long.")]
        [Display(Name = "Title")]
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
        public IFormFile File { get; set; }
        public string StatusMessage { get; set; }
    }
}
