using System;
using System.Collections.Generic;
using System.Collections;
using MySql.Data.MySqlClient;
using System.Linq;
using Microsoft.SqlServer;
using System.Data.SqlClient;

namespace WorkTest
{
    class Program
    {
        /// <summary>
        /// Sort Data from SQL by following scope
        /// </summary>
        /// <param name="SqlPath"> Must be like this for "MY":"server = ; SSL mode = ; port = ; user = root; database = ; password = "</param>
        /// <param name="Request"> Your SQL Request. Example: "SELECT Price FROM client" </param>
        /// <param name="Choose"> Choose Betwenn MS SQL and MySQL. Example: Enter MS for MS SQL and MY for MySQL </param>
        /// <param name="scope"> Scope for sort</param>
        public static void SortSqlData(string Choose, string SqlPath, string Request, (int, int) scope)
        {
            if (Choose == "MY")
            {
                List<string> array = new List<string>();

                string AddSql = SqlPath;

                using (MySqlConnection Connection = new MySqlConnection(AddSql))
                {
                    Connection.Open();

                    MySqlCommand command = new MySqlCommand(Request, Connection);

                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        array.Add(reader[0].ToString());

                    }
                    reader.Close();

                    Connection.Close();
                }
                var result = ConvertAndSort(array, scope);
                ShowResult(result);
            }
            else if (Choose == "MS")
            {
                List<string> array = new List<string>();

                string AddSql = SqlPath;
                using (SqlConnection Connection = new SqlConnection(SqlPath))
                {
                    Connection.Open();
                    Connection.Close();
                }
            }
            else
            {
                throw new Exception("Didn't choose a right SQL");
            }

        }
        /// <summary>
        /// Convert string Data from SQL to Int array and sort it 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="scope"></param>
        /// <returns></returns>
        public static int[] ConvertAndSort(List<string> array, (int, int) scope)
        {
            var array1 = Array.ConvertAll(array.ToArray(), t => Int32.Parse(t));

            var count = array1.Where(x => x >= scope.Item1 && x <= scope.Item2);

            var finalarray = count.ToArray();

            Array.Sort(finalarray);

            return finalarray;
        }
        /// <summary>
        /// Unnecessary method for getting result
        /// </summary>
        /// <param name="array"> Data to show </param>
        public static void ShowResult(int[] array)
        {
            foreach (int i in array)
            {
                Console.WriteLine(i);
            }
        }
        static void Main(string[] args)
        {
            SortSqlData("MY", "server = localhost; SSL mode = none; port = 3306; user = root; database = client; password = 0000", "SELECT Price FROM client", (200, 1000));
            string AddSql = "Data Source = database-1.cnfs5af4detk.us-east-2.rds.amazonaws.com; Integrated Security = true; Uid = admin; Password = Development-2021; Initial catalog = FTR";
            using (SqlConnection Connection = new SqlConnection(AddSql))
            {
                Connection.Open();
                Console.WriteLine("Good");
                Connection.Close();
            }
        }
    }
}
