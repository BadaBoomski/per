using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace personkartotekSLN
{
    public class AlternativeAdr
    {
        public Person altTilknyttetPerson { get; set; }

        public Adresse AltTilknyttetAdresse { get; set; }

        public String AAtype { get; set; }
    }
}