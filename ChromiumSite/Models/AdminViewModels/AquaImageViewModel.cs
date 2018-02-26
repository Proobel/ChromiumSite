using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ChromiumSite.Models.AdminViewModels
{
    public class AquaImageViewModel
    {
        public IFormFile File { get; set; }
        public string Path { get; set; }
        public int Id { get; set; }
        public bool Is_Template { get; set; }

    }
}

