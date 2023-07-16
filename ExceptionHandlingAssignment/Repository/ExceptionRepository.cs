using ExceptionHandlingAssignment.Interfaces;
using Microsoft.Data.SqlClient;

namespace ExceptionHandlingAssignment.Repository
{
    public class ExceptionRepository:IExceptionRepo
    {
        private readonly IConfiguration _configuration;
        public ExceptionRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public bool CheckSqlSyntax()
        {
            string connectionString = _configuration.GetConnectionString("con");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlQuery = "SELECT  FROM  YourTable";
                try
                {
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {

                        SqlDataReader reader = command.ExecuteReader();
                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
                return true;
            }
        }

        public bool CheckSqlConnection()
        {
            string connectionString = Constants.ConnectionString;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
