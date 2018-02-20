using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChromiumSite.Models.ManageViewModels
{
    public class CreateChromePropositionViewModel :CreatePropositionViewModel
    {
        [Display(Name = "Color")]
        public string Color { get; set; }
    }
}
