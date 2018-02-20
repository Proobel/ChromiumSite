using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChromiumSite.Models
{
    public class AquaImage
    {
        public int Id { get; set; }
        public string PathToImage { get; set; }
        public bool Is_Template { get; set; }
        public int AquaProposalId { get; set; }
        public AquaProposalModel AquaProposal { get; set; }
    }
}
