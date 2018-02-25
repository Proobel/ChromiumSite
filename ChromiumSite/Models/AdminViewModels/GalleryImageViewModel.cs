using Microsoft.AspNetCore.Http;

namespace ChromiumSite.Models.AdminViewModels
{
    public class GalleryImageViewModel
    {
        public IFormFile File { get; set; }
        public string Path { get; set; }
        public int Id { get; set; }
    }
}
