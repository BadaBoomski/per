using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient; // VIGTIGT!!


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

        /// <summary>
        /// Constructor may be use to initialize the connection string and likely setup things 
        /// </summary>
        public personkartotekDBUtil()
        {
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

                // For au442965-database (virker ikke...)
                @"Data Source = st - i4dab.uni.au.dk; Initial Catalog = E18I4DABau442965; User ID = E18I4DABau442965; Password = E18I4DABau442965; Connect Timeout = 30; Encrypt = False; TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover= False");
 
                // For localdb   
                // @"Data Source=(localdb)\ProjectsV13;Initial Catalog=personkartotekDataBase;Integrated Security=True;Pooling=False;Connect Timeout=30");
                con.Open();
                return con;
            }
        }

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
                per.personID = (long) cmd.ExecuteScalar(); //Returns the identity of the new tuple/record
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
    }
}
