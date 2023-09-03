using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementModules.Classes
{
    public class DatabaseOperations
    {
        private string _userName = "";
        private string _password = "";
        string _databaseName = "";
        bool _isAutoAuthentication = false;
        string _connectionString = "";
        SqlConnection _connection;
        SqlDataReader reader;

        public DatabaseOperations(string userName, string password, string databaseName, bool isAutoAuthentication)
        {
            _userName = userName;
            _password = password;
            _databaseName = databaseName;
            _isAutoAuthentication = isAutoAuthentication;

        }

        public bool Connect()
        {
            if (_isAutoAuthentication)
            {
                _connectionString = "Persist Security Info = False; Trusted_Connection = True; database = SchoolAdministration;  Data Source = .\\SQLEXPRESS";
            }
            else
            {
                _connectionString = "server = {local); Initial Catalog=Demodb;User ID=sa;Password=demol23";
            }
            _connection = new SqlConnection();
            _connection.ConnectionString = _connectionString;
            try
            {
                _connection.Open();
                return true;
            }
            catch (Exception ep)
            {
                return false;
            }

        }

        public SqlConnection SQLConnectionObject
        {
            get { return _connection; }
        }

        public SqlDataReader ExecuteQuery(string query)
        {
            
            SqlCommand command = new SqlCommand(query, _connection);
            reader = command.ExecuteReader();
            return reader;
        }

        public void CloseReader()
        {
            reader.Close();
        }
    }
}
