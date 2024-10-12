using Microsoft.Data.SqlClient;
using System.Data;
using Web_FIA44_Person_Webanwendung.ViewModels;

namespace Web_FIA44_Person_Webanwendung.DAL
{
	public class SqlDal : IAccessable
	{
		#region SQL-Connection
		// Private Verbindungsobjekt um SQL Server Verbindungen zu verwalten.
		private readonly SqlConnection conn;

		// Konstruktor zum Erstellen einer neuen SqlDal Instanz.	
		public SqlDal(string connectionString)
		{
			conn = new SqlConnection(connectionString);
		}
		#endregion
		#region Person hinzufügen
		public int InsertPerson(Person person)
		{
            //SQL-Befehlsstring zum Einfügen einer neuen Person in die Datenbank.
            string InsertQuery = "INSERT INTO PERSON (Firstname, Lastname, Birthday, Gender, Glasses, Remark) output inserted.PiD VALUES (@Firstname, @Lastname, @Birthday, @Gender, @Glasses, @Remark)";
            //SqlCommand Objekt zum Ausführen von SQL-Befehlen.
            SqlCommand insertCmd = new SqlCommand(InsertQuery, conn);
            //Parameter hinzufügen
            insertCmd.Parameters.AddWithValue("@Firstname", person.Firstname);
			insertCmd.Parameters.AddWithValue("@Lastname", person.Lastname);
            insertCmd.Parameters.AddWithValue("@Birthday", person.Birthday);
			insertCmd.Parameters.AddWithValue("@Gender", person.Gender);
			insertCmd.Parameters.AddWithValue("@Glasses", person.Glasses);
			insertCmd.Parameters.AddWithValue("@Remark", person.Remark);
            //Verbindung öffnen
            conn.Open();
            //ExecuteScalar Methode wird verwendet, um die erste Spalte der ersten Zeile in einem Resultset zurückzugeben.
            int newPid = (int)insertCmd.ExecuteScalar();
            //Verbindung schließen
            conn.Close();
            //Rückgabe der neuen Person ID
            return newPid;
		}
		#endregion
		#region Person nach ID ausgeben
		public Person GetPersonById(int PiD)
		{
            //SQL-Befehlsstring zum Auswählen einer Person anhand der ID.
            string SelectPersonById = "SELECT * FROM Person WHERE PiD = @PiD";
            //SqlCommand Objekt zum Ausführen von SQL-Befehlen.
            SqlCommand selectCmd = new SqlCommand(SelectPersonById, conn);
            //Parameter hinzufügen
            selectCmd.Parameters.AddWithValue("@PiD", PiD);
            //Verbindung öffnen
            conn.Open();
            //ExecuteReader Methode wird verwendet, um einen SqlDataReader zu erstellen.
            SqlDataReader reader = selectCmd.ExecuteReader();
            //Neues Person Objekt erstellen
            Person person = new Person();
            //Lesen der Daten aus dem Reader
            if (reader.Read())
			{
				person.PiD = (int)reader["PiD"];
				person.Firstname = reader["Firstname"].ToString();
				person.Lastname = reader["Lastname"].ToString();
				person.Birthday = ((DateTime)reader["Birthday"]);
				person.Gender = reader["Gender"].ToString();
				person.Glasses = (bool)reader["Glasses"];
				person.Remark = reader["Remark"].ToString();
            }
            //Verbindung schließen
            conn.Close();
            //Rückgabe der Person
            return person;
		}
		#endregion
		#region Alle Personen ausgeben
		public List<Person> GetAllPersons()
		{
            //SQL-Befehlsstring zum Auswählen aller Personen.
            string SelectAll = "SELECT * FROM Person";
            //SqlCommand Objekt zum Ausführen von SQL-Befehlen.
            SqlCommand selectCmd = new SqlCommand(SelectAll, conn);
            //Verbindung öffnen
            conn.Open();
            //ExecuteReader Methode wird verwendet, um einen SqlDataReader zu erstellen.
            SqlDataReader reader = selectCmd.ExecuteReader();
            //Neue Liste von Personen erstellen
            List<Person> Allpersons = new List<Person>();
            //Lesen der Daten aus dem Reader
            while (reader.Read())
			{
				Person person = new Person();
				person.PiD = (int)reader["PiD"];
				person.Firstname = reader["Firstname"].ToString();
				person.Lastname = reader["Lastname"].ToString();
				person.Birthday = ((DateTime)reader["Birthday"]); ;
				person.Gender = reader["Gender"].ToString();
				person.Glasses = (bool)reader["Glasses"];
				person.Remark = reader["Remark"].ToString();

				Allpersons.Add(person);
            }
            //Verbindung schließen
            conn.Close();
            //Rückgabe der Liste von Personen

            return Allpersons;
		}
		#endregion
		#region Person updaten
		public bool UpdatePerson(Person person)
		{
            //SQL-Befehlsstring zum Aktualisieren einer Person.
            string UpdatePerson = "UPDATE Person SET Firstname = @Firstname, Lastname = @Lastname, Birthday = @Birthday, Gender = @Gender, Glasses = @Glasses, Remark = @Remark WHERE PiD = @PiD";
            //SqlCommand Objekt zum Ausführen von SQL-Befehlen.
            SqlCommand updateCmd = new SqlCommand(UpdatePerson, conn);
            //Parameter hinzufügen
            updateCmd.Parameters.AddWithValue("@Firstname", person.Firstname);
			updateCmd.Parameters.AddWithValue("@Lastname", person.Lastname);
			updateCmd.Parameters.AddWithValue("@Birthday", person.Birthday);
			updateCmd.Parameters.AddWithValue("@Gender", person.Gender);
			updateCmd.Parameters.AddWithValue("@Glasses", person.Glasses);
			updateCmd.Parameters.AddWithValue("@Remark", person.Remark);
			updateCmd.Parameters.AddWithValue("@PiD", person.PiD);
            //Verbindung öffnen
            conn.Open();
            //ExecuteNonQuery Methode wird verwendet, um einen Befehl auszuführen, der keine Zeilen zurückgibt.
            int rows = updateCmd.ExecuteNonQuery();
            //Verbindung schließen
            conn.Close();
            //Rückgabe ob die Person aktualisiert wurde
            return rows == 1;
		}
		#endregion
		#region Person löschen
		public bool DeletePerson(int PiD)
		{
            //SQL-Befehlsstring zum Löschen einer Person.
            string DeletePerson = "DELETE FROM Person WHERE PiD = @PiD";
            //SqlCommand Objekt zum Ausführen von SQL-Befehlen.
            SqlCommand deleteCmd = new SqlCommand(DeletePerson, conn);
            //Parameter hinzufügen
            deleteCmd.Parameters.AddWithValue("@PiD", PiD);
            //Verbindung öffnen
            conn.Open();
            //ExecuteNonQuery Methode wird verwendet, um einen Befehl auszuführen, der keine Zeilen zurückgibt.
            int rows = deleteCmd.ExecuteNonQuery();
            //Verbindung schließen
            conn.Close();
            //Rückgabe ob die Person gelöscht wurde
            return rows == 1;
		}
        #endregion
        #region Person nach Suchindex ausgeben
        public List<Person> GetPersonBySearchIndex(string searchString)
		{
            //SQL-Befehlsstring zum Ausw?hlen einer Person nach einem Suchindex.
            //Der Suchindex wird durch eine '%' vor und nach dem Suchbegriff erweitert.
            //Dadurch werden auch Teilstrings gefunden, die den Suchbegriff enthalten.

            // TODO: SQL Injection vermeiden!	
			// SQL String erstellen		
			string SelectPersonBySearchIndex = "SELECT * FROM Person WHERE Firstname LIKE @searchString OR Lastname LIKE @searchString;";
            // Personenliste erstellen
            List<Person> Allpersons = new List<Person>();
            // SQL Command erstellen
            using (SqlCommand selectCmd = new SqlCommand(SelectPersonBySearchIndex, conn))
            {// Parameter hinzufügen
                selectCmd.Parameters.AddWithValue("@searchString", "%" + searchString + "%");
                // Verbindung öffnen
                conn.Open();
                // ExecuteReader Methode wird verwendet, um einen SqlDataReader zu erstellen.
                using (SqlDataReader reader = selectCmd.ExecuteReader())
				{
                    // Lesen der Daten aus dem Reader solange Daten vorhanden sind
                    while ( reader.Read())
					{
						Person person = new Person
						{
							PiD = (int)reader["PiD"],
							Firstname = reader["Firstname"].ToString(),
							Lastname = reader["Lastname"].ToString(),
							Birthday = ((DateTime)reader["Birthday"]),
							Gender = reader["Gender"].ToString(),
							Glasses = (bool)reader["Glasses"],
							Remark = reader["Remark"].ToString()
						};
                        // Person zur Liste hinzufügen
                        Allpersons.Add(person);
					}
				}
			}
            // Verbindung schließen
            conn.Close();
            // Rückgabe der Liste von Personen
            return Allpersons;
		}
        #endregion
    }
}

