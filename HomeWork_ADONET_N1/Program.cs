using System;
using System.Data.SqlClient;

namespace HomeWork_ADONET_N1
{
    //Для таблицы однотабличной БД из прошлого ДЗ реализовать набор базовых CRUD-операций в виде набора процедур.
    //Протестировать работу процедур (UI реализовывать не надо). 
    //Требуемые процедуры:
        //Получить все записи
        //Получить запись по id
        //Добавить новую запись
        //Удалить запись по id
        //Изменить запись по id
    //Приветствуется использование вспомогательных процедур.

    internal class Program
    {
        //Вспомогательные процедуры:
        //Процедура подключения к БД
        static SqlConnection OpenConnectionDb()
        {
            SqlConnection connection = new SqlConnection();
            string connectionString = @"Data Source=LENOVOBOOK\SQLEXPRESS; Initial Catalog=auto_db; Integrated Security=SSPI;";
            connection.ConnectionString = connectionString;
            connection.Open();
            return connection;
        }
        //Процедура вывода табличного результата
        static void SqlAnswer(SqlDataReader answer)
        {
            bool exist = true;
            while (answer.Read())
            {
                //title
                if (exist)
                {
                    for (int i = 0; i < answer.FieldCount - 1; i++)
                    {
                        Console.Write($"{answer.GetName(i)} - ");
                    }
                    Console.WriteLine(answer.GetName(answer.FieldCount - 1));
                }
                exist = false;
                //data
                for (int j = 0; j < answer.FieldCount - 1; j++)
                {
                    Console.Write($"{answer[j]} - ");
                }
                Console.WriteLine(answer[answer.FieldCount - 1]);
            }
            if (exist) Console.WriteLine("Data none.");
        }
        //Требуемые процедуры:
        // 1. Получить все записи
        static void SelectAll()
        {
            SqlConnection connection = null;
            SqlDataReader result = null;
            try
            {
                connection = OpenConnectionDb();
                SqlCommand query = new SqlCommand("select * from cars_t", connection);
                result = query.ExecuteReader();
                SqlAnswer(result);
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR!!! Something is broken: {e}");
            }
            finally
            {
                connection?.Close();
            }
        }
        // 2. Получить запись по Id
        static void SelectById(int id)
        {
            Console.WriteLine($"Data by id = {id}.");
            SqlConnection connection = null;
            SqlDataReader result = null;
            try
            {
                connection = OpenConnectionDb();
                SqlCommand query = new SqlCommand($"select * from cars_t where id = {id}", connection);
                result = query.ExecuteReader();
                SqlAnswer(result);
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR!!! Something is broken: {e}");
            }
            finally
            {
                connection?.Close();
            }
        }
        // 3. Добавить новую запись
        static void InsertRow(string brend_f, string model_f, int produced_in_f, int power_f, string country)
        {
            SqlConnection connection = null;
            try
            {
                connection = OpenConnectionDb();
                string insertRow = "insert into cars_t (brend_f, model_f, produced_in_f, power_f, country)" +
                    $" values ('{brend_f}', '{model_f}', {produced_in_f}, {power_f}, '{country}')";
                SqlCommand cmd = new SqlCommand(insertRow, connection);
                int result = cmd.ExecuteNonQuery();
                switch(result)
                {
                    case 0: Console.WriteLine("ERROR! Rows not inserted!"); break;
                    case 1: Console.WriteLine("OK! 1 rows inserted."); break;
                    default: Console.WriteLine($"ERROR! Something broken: {result}"); break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR!!! Something is broken: {e}");
            }
            finally
            {
                connection?.Close();
            }
        }
        // 4. Удалить запись по id
        static void DeleteById(int id)
        {
            SqlConnection connection = null;
            try
            {
                connection = OpenConnectionDb();
                SqlCommand cmd = new SqlCommand($"delete from cars_t where id = {id}", connection);
                int result = cmd.ExecuteNonQuery();
                switch (result)
                {
                    case 0: Console.WriteLine("ERROR! Rows not inserted!"); break;
                    case 1: Console.WriteLine("OK! 1 rows deleted."); break;
                    default: Console.WriteLine($"ERROR! Something broken: {result}"); break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR!!! Something is broken: {e}");
            }
            finally
            {
                connection?.Close();
            }
        }
        // 5. Изменить запись по id
        static void UpdateById(int id, string brend_f, string model_f, int produced_in_f, int power_f, string country)
        {
            SqlConnection connection = null;
            try
            {
                connection = OpenConnectionDb();
                string insertRow = $"update cars_t set brend_f = '{brend_f}', model_f = '{model_f}'," +
                    $" produced_in_f = {produced_in_f}, power_f = {power_f}, country = '{country}' where id = {id}";
                SqlCommand cmd = new SqlCommand(insertRow, connection);
                int result = cmd.ExecuteNonQuery();
                switch (result)
                {
                    case 0: Console.WriteLine("ERROR! Rows not updated!"); break;
                    case 1: Console.WriteLine("OK! 1 rows updated."); break;
                    default: Console.WriteLine($"ERROR! Something broken: updated {result} rows"); break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR!!! Something is broken: {e}");
            }
            finally
            {
                connection?.Close();
            }
        }
        static void Main(string[] args)
        {
            //Вспомогательная процедура подключения
            OpenConnectionDb();
            //1.Получить все записи
            SelectAll();
            //2.Получить запись по id
            SelectById(1000);
            SelectById(5);
            //3.Добавить новую запись
            InsertRow("Chery", "Tiggo 7pro", 2016, 122, "China");
            SelectAll();
            //4.Удалить запись по id
            DeleteById(12);
            SelectAll();
            //5.Изменить запись по id
            UpdateById(13, "Kia", "Mohave", 2008, 250, "Korea");
            SelectAll();
        }
    }
}
