using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using MenuBoards.Web.ViewModels;
using MySql.Data.MySqlClient;

namespace MenuBoards.DataAccess
{
    public class BaseRepository
    {

        public BaseRepository()
        {
            this.ConnectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;
        }

        public string ConnectionString { get; set; }

        public int ExecuteNonQuery(string query)
        {
            using (var connection = new MySqlConnection(this.ConnectionString))
            {
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.CommandType = CommandType.Text;
                    connection.Open();

                    var record = cmd.ExecuteNonQuery();

                    return record;
                }
            }
        }

        public MySqlDataReader ExecuteReader(string query)
        {
            using (var connection = new MySqlConnection(this.ConnectionString))
            {
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.CommandType = CommandType.Text;
                    connection.Open();

                    return cmd.ExecuteReader();
                }
            }
        }

        public string GenerateId()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}