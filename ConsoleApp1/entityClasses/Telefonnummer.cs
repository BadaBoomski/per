using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace personkartotekSLN
{
    public class Telefonnummer
    {
        public long telefonID { get; set; }

        public String teleselskab { get; set; }

        public String telefontype { get; set; }

        public int telefonnummer { get; set; }

        public Person tlfTilknyttetPerson { get; set; }

    }
}