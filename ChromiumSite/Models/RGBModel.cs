using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ChromiumSite.Models
{
    public class RGBModel
    {
        [Key]
        public int Id { get; set; }

        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }
        
    }
}
