using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Dapper;
using CsvHelper;
using CsvHelper.Configuration;
using System.IO;
using System.Windows.Controls;

namespace WpfApp2
{
    class Model
    {
        private string connectionString = @"Data Source=..\\..\\DataBase\\identifier.sqlite;";
        private SqliteConnection connection;
        
        public IEnumerable<Abonent> LoadData()
        {
            connection = new SqliteConnection(connectionString);
            try
            {
                
                string query = @"
                SELECT
                    A.surname,
                    A.name,
                    A.patronimic,
                    Ad.street,
                    Ad.house,
                    Ph.homeNumber,
                    Ph.workNumber,
                    Ph.mobileNumber
                FROM Abonent A
                INNER JOIN Address Ad ON A.id = Ad.abonent_id
                INNER JOIN PhoneNumber Ph ON A.id = Ph.abonent_id";

                IEnumerable<Abonent> abonents = connection.Query<Abonent>(query);
                return abonents;
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
            return null;
        }

        public IEnumerable<Street> LoadStreets()
        {
            connection = new SqliteConnection(connectionString);
            string query = @"
                    SELECT Streets.street_name AS StreetName,
                    COUNT(Address.abonent_id) AS NumberOfResidents
                    FROM Streets
                    INNER JOIN Address ON Streets.street_name = Address.street
                    GROUP BY Streets.street_name";
            IEnumerable<Street> str = connection.Query<Street>(query);
            return str;
        }

       public IEnumerable<Abonent> Search(string phoneNumber)
        {
            
            try
            {
                connection = new SqliteConnection(connectionString);
                    string query = @"
                SELECT
                    A.surname,
                    A.name,
                    A.patronimic,
                    Ad.street,
                    Ad.house,
                    Ph.homeNumber,
                    Ph.workNumber,
                    Ph.mobileNumber
                FROM Abonent A
                INNER JOIN Address Ad ON A.id = Ad.abonent_id
                INNER JOIN PhoneNumber Ph ON A.id = Ph.abonent_id
                WHERE Ph.homeNumber = @PhoneNumber OR
                     Ph.workNumber = @PhoneNumber OR
                     Ph.mobileNumber = @PhoneNumber";

                IEnumerable<Abonent> Phone = connection.Query<Abonent>(query, new { PhoneNumber = phoneNumber });
                return Phone;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return null;
        }

        public bool uploadCSVFile(ItemCollection dataItem)
        {

            DateTime thisMoment = DateTime.Now;
            string fileCSV = "..\\..\\report\\report_" + thisMoment.ToString("ddMMyyyy_HHmm") + ".csv";

            try
            {
                using (StreamWriter writer = new StreamWriter(fileCSV, false, Encoding.UTF8))
                using (CsvWriter csv = new CsvWriter(writer, new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)
                {
                    Delimiter = ";"
                }))
                {
                    csv.WriteField("Surname");
                    csv.WriteField("Name");
                    csv.WriteField("Patronymic");
                    csv.WriteField("Street");
                    csv.WriteField("House");
                    csv.WriteField("Home Number");
                    csv.WriteField("Work Number");
                    csv.WriteField("Mobile Number");
                    csv.NextRecord();

                    foreach (object item in dataItem)
                    {
                        if (item is Abonent abonent)
                        {
                            csv.WriteField(abonent.surname);
                            csv.WriteField(abonent.name);
                            csv.WriteField(abonent.patronimic);
                            csv.WriteField(abonent.street);
                            csv.WriteField(abonent.house);
                            csv.WriteField(abonent.homeNumber);
                            csv.WriteField(abonent.workNumber);
                            csv.WriteField(abonent.mobileNumber);
                            csv.NextRecord();
                        }
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public class Abonent
        {
            public string surname { get; set; }
            public string name { get; set; }
            public string patronimic { get; set; }
            public string street { get; set; }
            public int house { get; set; }
            public string homeNumber { get; set; }
            public string workNumber { get; set; }
            public string mobileNumber { get; set; }
        }

        public class Street
        {
            public string StreetName { get; set; }
            public int NumberOfResidents { get; set; }
        }

    }
}
