using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChromiumSite.Models
{
    public class Proposal
    {
        public int Id { get; set; }

        public DateTime Created_at { get; set; }
        [StringLength(50)]
        public string Status { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [StringLength(400)]
        public string Notes { get; set; }
    }
}
