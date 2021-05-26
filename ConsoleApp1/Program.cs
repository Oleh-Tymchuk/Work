using System;
using System.Collections.Generic;
using System.Collections;
using MySql.Data.MySqlClient;

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
            string AddSQl = "server=localhost; port=3307; user=root; database=client; password=1234";
            MySqlConnection Connection = new MySqlConnection(AddSQl);
            Connection.Open();
            string request = "SELECT id, Price FROM client WHERE PRICE > 200";
            MySqlCommand command = new MySqlCommand(request, Connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader[0].ToString() + " " + reader[1].ToString());
            }
            reader.Close();
            Connection.Close();
        }
    }
}
