using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain.Models
{
    public class Bloco
    {
        public Header header { get; set; }
        public Payload payload { get; set; }
    }
}
