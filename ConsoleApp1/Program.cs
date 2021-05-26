using System;
using System.Collections.Generic;
using System.Collections;
using MySql.Data.MySqlClient;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer;

namespace WorkTest
{
    public class Order : IComparable<Order>, IComparer<Order>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public DateTime Fulfillment { get; set; }
        public decimal Price { get; set; }
        public string PaymentMethod { get; set; }
        //public Specialist Specialist { get; set; }
        //public Client Client { get; set; }

        public int Compare(Order ord1, Order ord2)
        {
            if (ord1.Price > ord2.Price)
            {
                return 1;
            }
            else if (ord1.Price < ord2.Price)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        public int CompareTo(Order ord)
        {
            return Price.CompareTo(ord.Price);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<string> array = new List<string>();
            
            string AddSQl = "server=localhost; SSL mode = None; port=3306; user=root; database=client; password=0000";
            MySqlConnection Connection = new MySqlConnection(AddSQl); ;
            Connection.Open();
            string request = "SELECT Price FROM client WHERE Price > 200";
            MySqlCommand command = new MySqlCommand(request, Connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                array.Add(reader[0].ToString());

            }
            reader.Close();

            Connection.Close();

            var array1 = Array.ConvertAll<string, int>(array.ToArray(), t => Int32.Parse(t));

            Array.Sort(array1);

            foreach (int i in array1)
            {
                Console.WriteLine(i);
            }
            Console.ReadLine();
        }
    }
}
