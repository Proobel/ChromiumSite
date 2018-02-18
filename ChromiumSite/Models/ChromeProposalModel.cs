using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChromiumSite.Models
{
    public class ChromeProposalModel : Proposal
    {
        public int? ColorId { get; set; }
        public RGBModel Color { get; set; }
    }
}
