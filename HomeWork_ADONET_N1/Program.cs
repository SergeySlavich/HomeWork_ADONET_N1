using System;
using System.Data.SqlClient;

namespace HomeWork_ADONET_N1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection();
                string connectionString = @"Data Source=LENOVOBOOK\SQLEXPRESS; Initial Catalog=auto_db; Integrated Security=SSPI;";
                connection.ConnectionString = connectionString;
                connection.Open();
                Console.WriteLine("Connection is opened.");
                string queryString = "select * from cars_t";
                SqlCommand SqlCmd = new SqlCommand(queryString, connection);
                SqlDataReader result = SqlCmd.ExecuteReader();

                for (int i = 0; i < result.FieldCount - 1; i++)
                {
                    Console.Write(result.GetName(i));
                    if(i != result.FieldCount - 2) Console.Write(" - ");
                }
                Console.WriteLine();
                while (result.Read())
                {
                    for (int j = 0; j < result.FieldCount - 1; j++)
                    {
                        Console.Write(result[j]);
                        if (j != result.FieldCount - 2) Console.Write(" - ");
                    }
                    Console.WriteLine();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(@"Error! Что-то сломалось: {e}");
            }
            finally
            {
                connection?.Close();
                Console.WriteLine("Connection closed.");
            }
        }
    }
}
