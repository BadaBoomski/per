using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

// VIGTIGT!!


namespace personkartotekSLN
{
    /// <summary>
    /// The naming concention in this class is both English and Danish. 
    /// The danish terms for entities has been preserved in the code
    /// to be in accordance with RDB.
    /// </summary>
    public class personkartotekDBUtil
    {
        private Person currentPerson;
        private Adresse currentAdresse;
        private PostnrBy currentPostnrBy;
        private Notat currentNotat;
        private Telefonnummer currentTelefonnummer;
        private AlternativeAdr currentAlternativeAdr;

        /// <summary>
        /// Constructor may be use to initialize the connection string and likely setup things 
        /// </summary>
        public personkartotekDBUtil()
        {

            // initializing  defaults constructors

            currentAdresse = new Adresse()
            {
                adresseID = 0,
                vejnavn = "",
            };

            currentPostnrBy = new PostnrBy()
            {
                postnrbyID = 0,
                bynavn = "",
                land = "",
                postnr = ""
            };

            currentNotat = new Notat()
            {
                notatID = 0,
                notat = "",
                notatTilknyttetPerson = null
            };

            currentTelefonnummer = new Telefonnummer()
            {
                telefonID = 0,
                telefonnummer = 0,
                teleselskab = "",
                telefontype = "",
                tlfTilknyttetPerson = null
            };

            currentAlternativeAdr = new AlternativeAdr()
            {
                AAtype = "",
                AltTilknyttetAdresse = null,
                altTilknyttetPerson = null
            };

            currentPerson = new Person()
            {
                personID = 0,
                Fornavn = "",
                Mellemnavn = "",
                Efternavn = "",
                Persontype = "",
                //adresseID = 0
            };
        }



        /// <summary>
        /// This a local utility method providing an opne ADO.NET SQL connection
        /// Examples of connection strings are given like below:
        /// new SqlConnection(@"Data Source=(localdb)\\Projects;Initial Catalog=Opgave1;Integrated Security=True");
        /// new SqlConnection(@"Data Source=(local);Initial Catalog=Northwind;Integrated Security=SSPI");
        /// new SqlConnection(@"Data Source=webhotel10.iha.dk;Initial Catalog=E13I4DABH0Gr1;User ID=E13I4DABH0Gr1; Password=E13I4DABH0Gr1");
        /// new SqlConnection(@"Data Source=(localdb)\ProjectsV13;User ID=Program;Password=Program123;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        /// </summary>
        private SqlConnection OpenConnection
        {
            get
            {
                //var con = new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=CraftManDB;Integrated Security=True");
                var con = new SqlConnection(

                 // For au442965-database (virker ikke...) // NOTICE: tried removing white space in Data Source. Tested and works!!
                 @"Data Source =st-i4dab.uni.au.dk; Initial Catalog = E18I4DABau442965; User ID = E18I4DABau442965; Password = E18I4DABau442965; Connect Timeout = 30; Encrypt = False; TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover= False");

                 // For localdb   
                 // @"Data Source=(localdb)\ProjectsV13;Initial Catalog=personkartotekDataBase;Integrated Security=True;Pooling=False;Connect Timeout=30");
                con.Open();
                return con;
            }
        }

        // Below is shown the CRUD operations for all entities.

        #region Person CRUD

        /// <summary>.
        /// 
        /// </summary>
        /// <param name="per">Person som skal tilføjes påvirker ikke currentPerson</param>
        public void AddPersonDB(ref Person per)
        {
            // prepare command string using paramters in string and returning the given identity 

            string insertStringParam = @"INSERT INTO Person(fornavn, mellemnavn, efternavn, persontype, adresseID)
                                                    OUTPUT INSERTED.personID  
                                                    VALUES (@Fornavn,@Mellemnavn,@Efternavn,@Persontype, @adresseID)";

            using (SqlCommand cmd = new SqlCommand(insertStringParam, OpenConnection))
            {
                // Get your parameters ready                    
                cmd.Parameters.AddWithValue("@fornavn", per.Fornavn);
                cmd.Parameters.AddWithValue("@mellemnavn", per.Mellemnavn);
                cmd.Parameters.AddWithValue("@efternavn", per.Efternavn);
                cmd.Parameters.AddWithValue("@persontype", per.Persontype);
                cmd.Parameters.AddWithValue("@adresseID", per.FolkeregisterAdresse.adresseID);
                per.personID = (long)cmd.ExecuteScalar(); //Returns the identity of the new tuple/record
            }
        }

        public void GetPersonByFirstAndLastName(ref Person per)
        {
            string sqlcmd = @"SELECT  TOP 1 * FROM Person WHERE (Fornavn = @fornavn) AND (Efternavn=@efternavn)";
            using (var cmd = new SqlCommand(sqlcmd, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@fornavn", per.Fornavn);
                cmd.Parameters.AddWithValue("@efternavn", per.Efternavn);
                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {

                    currentPerson.personID = (int)rdr["personID"];
                    currentPerson.Fornavn = (string)rdr["Fornavn"];
                    currentPerson.Mellemnavn = (string)rdr["Mellemnavn"];
                    currentPerson.Efternavn = (string)rdr["Efternavn"];
                    currentPerson.Persontype = (string)rdr["Persontype"];
                    per = currentPerson;
                }
            }
        }

        public void UpdatePersonDB(ref Person per)
        {
            // prepare command string
            string updateString =
                @"UPDATE Person
                        SET Fornavn = @fornavn, Mellemnavn = @mellemnavn, Efternavn = @efternavn, Persontype = @persontype
                        WHERE personID = @personID";

            using (SqlCommand cmd = new SqlCommand(updateString, OpenConnection))
            {
                // Get your parameters ready 
                cmd.Parameters.AddWithValue("@fornavn", per.Fornavn);
                cmd.Parameters.AddWithValue("@mellemnavn", per.Mellemnavn);
                cmd.Parameters.AddWithValue("@efternavn", per.Efternavn);
                cmd.Parameters.AddWithValue("@persontype", per.Persontype);
                cmd.Parameters.AddWithValue("@personID", per.personID);

                var id = (int)cmd.ExecuteNonQuery();
            }
        }

        public void DeletePersonDB(ref Person per)
        {
            string deleteString = @"DELETE FROM Person WHERE (personID = @personID)";
            using (SqlCommand cmd = new SqlCommand(deleteString, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@personID", per.personID);

                var id = (int)cmd.ExecuteNonQuery();
                per = null;
            }
        }

        #endregion

        #region Adresse CRUD

        /// <summary>.
        /// 
        /// </summary>
        /// <param name="adr">Adresse som skal tilføjes påvirker ikke currentPerson</param>
        public void AddAdressDB(ref Adresse adr)
        {
            // prepare command string using paramters in string and returning the given identity 

            string insertStringParam = @"INSERT INTO Adresse(vejnavn, postnrbyID)
                                                    OUTPUT INSERTED.adresseID  
                                                    VALUES (@vejnavn, @postnrbyID)";

            using (SqlCommand cmd = new SqlCommand(insertStringParam, OpenConnection))
            {
                // Get your parameters ready                    
                cmd.Parameters.AddWithValue("@vejnavn", adr.vejnavn);
                cmd.Parameters.AddWithValue("@postnrbyID", adr.adrTilknyttetPostnrBy.postnrbyID);

                adr.adresseID = (long)cmd.ExecuteScalar(); //Returns the identity of the new tuple/record
            }
        }

        public List<Adresse> GetAllAdresses()
        {
            string sqlcmd = @"SELECT * FROM Adresse";
            using (var cmd = new SqlCommand(sqlcmd, OpenConnection))
            {
                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();
                List<Adresse> allAdresses = new List<Adresse>();
                Adresse adr = new Adresse();

                while (rdr.Read())
                {
                    adr = new Adresse();
                    adr.adresseID = (long)rdr["adresseID"];
                    adr.vejnavn = (string)rdr["vejnavn"];
                    allAdresses.Add(adr);
                }

                return allAdresses;
            }
        }

        // SE BORT FRA NEDENSTÅENDE METODE. KUNNE IKKE FÅ DEN TIL AT VIRKE INDEN DEADLINE.
        // public List<Adresse> GetAdresseContainingString(string search)
        //// public void GetAdresseContainingString(ref Adresse adr)
        // {
        //     // string sqlcmd = @"SELECT * FROM Adresse WHERE ([vejnavn] = @vejnavn) LIKE %@search%"; // Kunne desværre ikke få den til at virke inden deadline...
        //     using (var cmd = new SqlCommand(sqlcmd, OpenConnection))
        //     {
        //         Adresse adr = null;
        //         SqlDataReader rdr = null;
        //         //cmd.Parameters.AddWithValue("@vejnavn", adr.vejnavn);
        //         rdr = cmd.ExecuteReader();
        //         List<Adresse> hverAdresse = new List<Adresse>();
        //         // cmd.Parameters.AddWithValue("@vejnavn", adr.vejnavn);
        //         //cmd.Parameters.AddWithValue("@search", "search");

        //         while (rdr.Read())
        //         {
        //             adr = new Adresse();
        //             adr.adresseID = (int)rdr["adresseID"];
        //             adr.vejnavn = (string)rdr["vejnavn"];
        //             hverAdresse.Add(adr);
        //         }

        //         return hverAdresse;
        //     }
        // }

        public void UpdateAdresseDB(ref Adresse adr)
        {
            // prepare command string
            string updateString =
                @"UPDATE Adresse
                        SET vejnavn = @vejnavn
                        WHERE adresseID = @adresseID";

            using (SqlCommand cmd = new SqlCommand(updateString, OpenConnection))
            {
                // Get your parameters ready 
                cmd.Parameters.AddWithValue("@vejnavn", adr.vejnavn);
                cmd.Parameters.AddWithValue("@adresseID", adr.adresseID);

                var id = (int)cmd.ExecuteNonQuery();
            }
        }

        public void DeleteAdresseDB(ref Adresse adr)
        {
            string deleteString = @"DELETE FROM Adresse WHERE (adresseID = @adresseID)";
            using (SqlCommand cmd = new SqlCommand(deleteString, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@adresseID", adr.adresseID);

                var id = (int)cmd.ExecuteNonQuery();
                adr = null;
            }
        }
        #endregion

        #region Postnrby CRUD

        public void AddPostnrbyDB(ref PostnrBy poby)
        {
            // prepare command string using paramters in string and returning the given identity 

            string insertStringParam = @"INSERT INTO PostnrBy(postnr, bynavn, land)
                                                    OUTPUT INSERTED.postnrbyID  
                                                    VALUES (@postnr, @bynavn, @land)";

            using (SqlCommand cmd = new SqlCommand(insertStringParam, OpenConnection))
            {
                // Get your parameters ready                    
                cmd.Parameters.AddWithValue("@postnr", poby.postnr);
                cmd.Parameters.AddWithValue("@bynavn", poby.bynavn);
                cmd.Parameters.AddWithValue("@land", poby.land);


                poby.postnrbyID = (long)cmd.ExecuteScalar(); //Returns the identity of the new tuple/record
            }
        }
        public void GetPostnrByByLand(ref PostnrBy pos)
        {
            string sqlcmd = @"SELECT * FROM PostnrBy WHERE (land = @land)";
            using (var cmd = new SqlCommand(sqlcmd, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@land", pos.land);
                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {

                    currentPostnrBy.postnrbyID = (long)rdr["postnrbyID"];
                    currentPostnrBy.bynavn = (string)rdr["bynavn"];
                    currentPostnrBy.land = (string)rdr["land"];
                    currentPostnrBy.postnr = (string)rdr["postnr"];
                    pos = currentPostnrBy;
                }
            }
        }

        public void UpdatePostnrByDB(ref PostnrBy poby)
        {
            // prepare command string
            string updateString =
                @"UPDATE PostnrBy
                        SET vejnavn = @vejnavn, Mellemnavn = @mellemnavn, Efternavn = @efternavn, Persontype = @persontype
                        WHERE personID = @personID";

            using (SqlCommand cmd = new SqlCommand(updateString, OpenConnection))
            {
                // Get your parameters ready 
                cmd.Parameters.AddWithValue("@postnr", poby.postnr);
                cmd.Parameters.AddWithValue("@bynavn", poby.bynavn);
                cmd.Parameters.AddWithValue("@land", poby.land);
                cmd.Parameters.AddWithValue("@postnrbyID", poby.postnrbyID);

                var id = (int)cmd.ExecuteNonQuery();
            }
        }

        public void DeletePostnrBy(ref PostnrBy poby)
        {
            string deleteString = @"DELETE FROM PostnrBy WHERE (postnrbyID = @postnrbyID)";
            using (SqlCommand cmd = new SqlCommand(deleteString, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@personID", poby.postnrbyID);

                var id = (int)cmd.ExecuteNonQuery();
                poby = null;
            }
        }
        #endregion

        #region Notat CRUD

        public void AddNotatDB(ref Notat nota)
        {
            // prepare command string using paramters in string and returning the given identity 

            string insertStringParam = @"INSERT INTO Notat(notat)
                                                    OUTPUT INSERTED.notatID  
                                                    VALUES (@notat)";

            using (SqlCommand cmd = new SqlCommand(insertStringParam, OpenConnection))
            {
                // Get your parameters ready                    
                cmd.Parameters.AddWithValue("@notat", nota.notat);
                nota.notatID = (long)cmd.ExecuteScalar(); //Returns the identity of the new tuple/record
            }
        }

        public List<Notat> GetNotatByOrder()
        {
            string sqlcmd = @"SELECT * FROM Notat ORDER BY (notat = @notat)";
            using (var cmd = new SqlCommand(sqlcmd, OpenConnection))
            {
                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();
                List<Notat> orderedNotatList = new List<Notat>();
                Notat nota = new Notat();

                while (rdr.Read())
                {
                    nota = new Notat();
                    nota.notatID = (long)rdr["notatID"];
                    nota.notat = (string)rdr["notat"];
                    orderedNotatList.Add(nota);
                }

                return orderedNotatList;
            }
        }

        public void UpdateNotatDB(ref Notat nota)
        {
            // prepare command string
            string updateString =
                @"UPDATE Notat
                        SET notat = @notat, notatID = @notatID
                        WHERE personID = @personID";

            using (SqlCommand cmd = new SqlCommand(updateString, OpenConnection))
            {
                // Get your parameters ready 
                cmd.Parameters.AddWithValue("@notat", nota.notat);
                cmd.Parameters.AddWithValue("@notatID", nota.notatID);


                var id = (int)cmd.ExecuteNonQuery();
            }
        }

        public void DeleteNotat(ref Notat nota)
        {
            string deleteString = @"DELETE FROM Notat WHERE (notatID = @notatID)";
            using (SqlCommand cmd = new SqlCommand(deleteString, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@notatID", nota.notatID);

                var id = (int)cmd.ExecuteNonQuery();
                nota = null;
            }
        }
        #endregion

        #region Telefonnummer CRUD

        public void AddTelefonnummerDB(ref Telefonnummer telef)
        {
            // prepare command string using paramters in string and returning the given identity 

            string insertStringParam = @"INSERT INTO Telefonnummer(telefonnummer, telefontype, teleselskab)
                                                    OUTPUT INSERTED.telefonID  
                                                    VALUES (@telefonnummer, @telefontype, @teleselskab)";

            using (SqlCommand cmd = new SqlCommand(insertStringParam, OpenConnection))
            {
                // Get your parameters ready                    
                cmd.Parameters.AddWithValue("@telefonnummer", telef.telefonnummer);
                cmd.Parameters.AddWithValue("@telefontype", telef.telefontype);
                cmd.Parameters.AddWithValue("@teleselskab", telef.teleselskab);
                telef.telefonID = (long)cmd.ExecuteScalar(); //Returns the identity of the new tuple/record
            }
        }

        public List<Telefonnummer> GetTelefonnummerByOrder()
        {
            string sqlcmd = @"SELECT * FROM Telefonnummer ORDER BY (telefonnummer = @telefonnummer)";
            using (var cmd = new SqlCommand(sqlcmd, OpenConnection))
            {
                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();
                List<Telefonnummer> orderedEmailList = new List<Telefonnummer>();
                Telefonnummer telef = new Telefonnummer();

                while (rdr.Read())
                {
                    telef = new Telefonnummer();
                    telef.telefonID = (long)rdr["telefonID"];
                    telef.telefonnummer = (int)rdr["telefonnummer"];
                    telef.telefontype = (string)rdr["telefontype"];
                    telef.teleselskab = (string)rdr["teleselskab"];
                }
                return orderedEmailList;
            }
        }

        public void UpdateTelefonnummerDB(ref Telefonnummer telef)
        {
            // prepare command string
            string updateString =
                @"UPDATE Telefonnummer
                        SET telefonnummer = @telefonnummer, telefontype = @telefontype, teleselskab = @teleselskab, telefonID = @telefonID
                        WHERE personID = @personID";

            using (SqlCommand cmd = new SqlCommand(updateString, OpenConnection))
            {
                // Get your parameters ready 
                cmd.Parameters.AddWithValue("@telefonnummer", telef.telefonnummer);
                cmd.Parameters.AddWithValue("@telefontype", telef.telefontype);
                cmd.Parameters.AddWithValue("@teleselskab", telef.teleselskab);
                cmd.Parameters.AddWithValue("@telefonID", telef.telefonID);

                var id = (int)cmd.ExecuteNonQuery();
            }
        }

        public void DeleteTelefonnummer(ref Telefonnummer telef)
        {
            string deleteString = @"DELETE FROM Telefonnummer WHERE (telefonID = @telefonID)";
            using (SqlCommand cmd = new SqlCommand(deleteString, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@telefonID", telef.telefonID);

                var id = (int)cmd.ExecuteNonQuery();
                telef = null;
            }
        }
        #endregion

        #region Email CRUD

        public void AddEmailDB(ref Email ema)
        {
            // prepare command string using paramters in string and returning the given identity 

            string insertStringParam = @"INSERT INTO Email(email)
                                                    OUTPUT INSERTED.emailID  
                                                    VALUES (@email)";

            using (SqlCommand cmd = new SqlCommand(insertStringParam, OpenConnection))
            {
                // Get your parameters ready                    
                cmd.Parameters.AddWithValue("@email", ema.email);
                ema.emailID = (long)cmd.ExecuteScalar(); //Returns the identity of the new tuple/record
            }
        }

        public List<Email> GetEmailByOrder()
        {
            string sqlcmd = @"SELECT * FROM Email ORDER BY (email = @email)";
            using (var cmd = new SqlCommand(sqlcmd, OpenConnection))
            {
                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();
                List<Email> orderedEmailList = new List<Email>();
                Email ema = new Email();

                while (rdr.Read())
                {
                    ema = new Email();
                    ema.emailID = (long)rdr["emailID"];
                    ema.email = (string)rdr["email"];
                    orderedEmailList.Add(ema);
                }
                return orderedEmailList;
            }
        }

        public void UpdateEmailDB(ref Email ema)
        {
            // prepare command string
            string updateString =
                @"UPDATE Email
                        SET email = @email, emailID = @emailID
                        WHERE personID = @personID";

            using (SqlCommand cmd = new SqlCommand(updateString, OpenConnection))
            {
                // Get your parameters ready 
                cmd.Parameters.AddWithValue("@email", ema.email);
                cmd.Parameters.AddWithValue("@emailID", ema.emailID);


                var id = (int)cmd.ExecuteNonQuery();
            }
        }

        public void DeleteEmail(ref Email ema)
        {
            string deleteString = @"DELETE FROM Email WHERE (emailID = @emailID)";
            using (SqlCommand cmd = new SqlCommand(deleteString, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@emailID", ema.emailID);

                var id = (int)cmd.ExecuteNonQuery();
                ema = null;
            }
        }
        #endregion

    }
}
