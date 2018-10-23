using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace personkartotekSLN
{
    class personApp
    {
        public void TheApp()
        {
            personkartotekDBUtil perutil = new personkartotekDBUtil();

            // Below the CRUD-operations are shown. Press the + on the left to open.
            // Remember to comment the other CRUD's out when testing.

            #region AddpersonDB (but first add postnrby and adresse)
            // Think scaffolding diagram - to add a person, we first need have a PostnrBy and a Adresse.
            // In this "case" we don't, so we start by creating them. Another solution would be to add some to the database and "pull" from there.

            //PostnrBy nyBy = new PostnrBy()
            //{
            //    bynavn = "Viby J",
            //    land = "Danmark",
            //    postnr = "8260",
            //    postnrbyID = 1
            //};
            //perutil.AddPostnrbyDB(ref nyBy);

            //Adresse nyAdresse1 = new Adresse()
            //{
            //    adrTilknyttetPerson = null,
            //    adresseID = 1,
            //    adrTilknyttetPostnrBy = nyBy,
            //    vejnavn = "Finlandsvej 500"
            //};
            //perutil.AddAdressDB(ref nyAdresse1);

            //Person per = new Person()
            //{
            //    personID = 1,
            //    Fornavn = "Maja",
            //    Mellemnavn = "Lind",
            //    Efternavn = "Thistrup",
            //    Persontype = "Mor",
            //    FolkeregisterAdresse = nyAdresse1
            //};
            //perutil.AddPersonDB(ref per);

            #endregion Remember to comment out the other regions to use/demonstrate this.

            #region UpdatePersonDB

            //PostnrBy nyBy2 = new PostnrBy()
            //{
            //    bynavn = "Viby J",
            //    land = "Danmark",
            //    postnr = "8260",
            //    postnrbyID = 1
            //};

            //Adresse nyAdresse2 = new Adresse()
            //{
            //    adrTilknyttetPerson = null,
            //    adresseID = 2,
            //    adrTilknyttetPostnrBy = nyBy2,
            //    vejnavn = "Finlandsgade 15, 3. th"
            //};

            //Person perUpdate = new Person()
            //{
            //    personID = 35,
            //    Fornavn = "JensUpdate",
            //    Mellemnavn = "PeterUpdate",
            //    Efternavn = "NielsenUpdate",
            //    Persontype = "KollegaUpdate",
            //    FolkeregisterAdresse = nyAdresse2
            //};

            //perutil.UpdatePersonDB(ref perUpdate);

            #endregion Remember to comment out the other regions to use/demonstrate this.

            #region GetPersonByFirstAndLastName

            //Person perRead = new Person() {Fornavn = "Finn", Efternavn = "Holgersen"};

            //perutil.GetPersonByFirstAndLastName(ref perRead);

            #endregion

            #region DeletePersonDB

            //Person perDeleteTest = new Person()
            //{
            //    personID = 22,

            //};
            //perutil.DeletePersonDB(ref perDeleteTest);

            #endregion Remember to comment out the other regions to use/demonstrate this.

            #region UpdateAdresseDB

            //Adresse adrUpdate = new Adresse()
            //{
            //    adresseID = 4,
            //    vejnavn = "Opdateretvejnavn 13, st. th",
            //};

            //perutil.UpdateAdresseDB(ref adrUpdate);

            #endregion

            #region DeleteAdresseDB

            //Adresse deleteAdresseTest = new Adresse()
            //{
            //    adresseID = 7,
            //};

            //perutil.DeleteAdresseDB(ref deleteAdresseTest);

            #endregion


            return;

        }
    }
}

