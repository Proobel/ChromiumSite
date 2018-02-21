using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChromiumSite.Models.AdminViewModels
{
    public class UserListViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool RoleName { get; set; }
        public string PhoneNumber { get; set; }
        public string StatusMessage { get; set; }
    }
}
