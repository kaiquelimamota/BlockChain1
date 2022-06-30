using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain.Models
{
    public class blocoMinerado
    {
        public Bloco bloco { get; set; }
        public string hash { get; set; }
        public string hashCurto { get; set; }
        public TimeSpan tempoDeMineração { get; set; }
    }
}
