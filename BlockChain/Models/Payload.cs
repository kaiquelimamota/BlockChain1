using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain.Models
{
    public class Payload
    {
        public int sequencia { get; set; }
        public string timestamp { get; set; }
        public string dados { get; set; }
        public string hashAnterior { get; set; }
    }
}
