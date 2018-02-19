using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChromiumSite.Models.ManageViewModels
{
    public class CreateAquaPropositionViewModel :CreatePropositionViewModel
    {
        public IFormFile File { get; set; }
    }
}
