using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChromiumSite.Models
{
    public class HomeBlog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }

        public DateTime Created_at { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
