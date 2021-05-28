using System;
using System.Collections.Generic;
using System.Collections;
using MySql.Data.MySqlClient;
using System.Linq;
using Microsoft.SqlServer;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Dapper;

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
        public static void SortSqlData(string SqlPath, string Request, (int, int) scope)
        {
            int[] ConvertAndSort(List<string> array1, (int, int) scope1)
            {
                var array2 = Array.ConvertAll(array1.ToArray(), t => Int32.Parse(t));

                var count = array2.Where(x => x >= scope.Item1 && x <= scope.Item2);

                var finalarray = count.ToArray();

                Array.Sort(finalarray);

                return finalarray;
            }

            List<string> array = new List<string>();

            using (SqlConnection conect = new SqlConnection(SqlPath))
            {
                conect.Open();

                var command = new SqlCommand(Request, conect);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    array.Add(reader[0].ToString());

                    Console.WriteLine(reader[0].ToString());
                }
                conect.Close();
            }
            var result = ConvertAndSort(array, scope);

        }
        static void Main(string[] args)
        {
            SortSqlData(@"Data Source = database-1.cnfs5af4detk.us-east-2.rds.amazonaws.com; Initial Catalog=FTR; User id = admin; Password =Development-2021", "SELECT Price FROM dbo.Services", (0, 1000));
        }
    }
}
