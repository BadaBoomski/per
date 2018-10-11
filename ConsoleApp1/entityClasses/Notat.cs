using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

namespace personkartotekSLN
{
    public class Notat
    {
        public long notatID { get; set; }

        public String notat { get; set; }

        public Person notatTilknyttetPerson { get; set; }
    }
}