﻿using Microsoft.AspNetCore.Http;

namespace ChromiumSite.Models.AdminViewModels
{
    public class GalleryImageViewModel
    {
        public IFormFile File { get; set; }
        public string Alt { get; set; }
    }
}
