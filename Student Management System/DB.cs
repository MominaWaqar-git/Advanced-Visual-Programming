using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System
{ 
        class DB
        {
            private static string connStr =
                "server=localhost;database=StudentDB;uid=root;pwd=;";

            public static MySqlConnection GetConnection()
            {
                return new MySqlConnection(connStr);
            }

            public static void Execute(string query)
            {
                using var conn = GetConnection();
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }

            public static MySqlDataReader Read(string query)
            {
                var conn = GetConnection();
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            }
        }
    }
