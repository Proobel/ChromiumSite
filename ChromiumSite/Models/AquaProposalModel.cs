using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChromiumSite.Models
{
    public class AquaProposalModel : Proposal
    {

        public int ImageID { get; set; }
        public AquaImage Image { get; set; }
    }
}
