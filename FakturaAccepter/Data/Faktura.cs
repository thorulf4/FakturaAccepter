using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakturaAccepter.Data
{
    public class Faktura
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime TimeAdded { get; set; }
        public FakturaState State { get; set; }
    }
}
