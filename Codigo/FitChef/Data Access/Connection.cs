using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;

namespace FitChef.Data_Access
{
    public class Connection 
    {
        private SqlConnection _connection;

        public Connection()
        {
            //_connection = new SqlConnection(....);
            string stringconnection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\luisb\Desktop\LI4\Base de dados\FitChef.mdf;Integrated Security=True;Connect Timeout=30;MultipleActiveResultSets=true;
";
            _connection = new SqlConnection(stringconnection);
        }

        public void Close()
        {
            if (_connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }

        public SqlConnection Fetch()
        {
            if (_connection.State == ConnectionState.Open)
            {
                return _connection;
            }
            else
            {
                return this.Open();
            }
        }

        public SqlConnection Open()
        {

            _connection.Open();
            return _connection;
        }
    }
}