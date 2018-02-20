using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChromiumSite.Models.ManageViewModels
{
    public class CreatePropositionViewModel
    {
        [StringLength(400,ErrorMessage = "The {0} must be at max {1} characters long.")]
        [Display(Name = "Notes")]
        public string Notes { get; set; }

        public string StatusMessage { get; set; }
    }
}
