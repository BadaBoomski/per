using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace personkartotekSLN
{
    public class Adresse
    {
        public long adresseID { get; set; }

        public PostnrBy adrTilknyttetPostnrBy { get; set; }
        
        public String vejnavn { get; set; }

        public Person adrTilknyttetPerson { get; set; }
    }
}