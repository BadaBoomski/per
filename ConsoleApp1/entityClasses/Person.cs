using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace personkartotekSLN
{
    public class Person
    {
        public String Fornavn { get; set; }

        public String Mellemnavn { get; set; }

        public String Efternavn { get; set; }

        public String Persontype { get; set; }

        public long personID { get; set; }

        public List<AlternativeAdr> AlternativeAdresser { get; set; }

        public Adresse FolkeregisterAdresse { get; set; }

        public Telefonnummer EjerTelefonnummer { get; set; }

        public Notat HarNotat { get; set; }

        public Email HarEmail { get; set; }



    }
}