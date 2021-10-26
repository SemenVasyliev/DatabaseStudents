using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

#region User Manual

/* User Manual
 * 
 * 1. Выход exit
 * 
 */

#endregion

namespace DatabaseStudents
{
    class Program
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["StudentsDB"].ConnectionString;

        private static SqlConnection sqlConnection = null;

        static void Main(string[] args)
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            Console.WriteLine("StudentsApp");

            SqlDataReader sqlDataReader = null;

            string command = string.Empty;

            while (true)
            {
                Console.Write("> ");
                command = Console.ReadLine();

                #region Exit
                // Exit the Programm
                if (command.ToLower().Equals("exit"))
                {
                    if (sqlConnection.State == ConnectionState.Open)
                    {
                        sqlConnection.Close();
                    }

                    if (sqlDataReader != null)
                    {
                        sqlDataReader.Close();
                    }

                    break;
                }
                #endregion 

                SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);

                switch (command.Split(' ')[0].ToLower())
                {
                    case "select":

                        sqlDataReader = sqlCommand.ExecuteReader();

                        while (sqlDataReader.Read())
                        {
                            Console.WriteLine($"{sqlDataReader["Id"]} {sqlDataReader["FIO"]}" +
                                $"{sqlDataReader["Birthday"]} {sqlDataReader["University"]}" +
                                $"{sqlDataReader["Group_number"]} {sqlDataReader["Course"]} {sqlDataReader["Average_score"]}");

                            Console.WriteLine(new string('-', 30));
                        }

                        if (sqlDataReader != null)
                        {
                            sqlDataReader.Close();
                        }

                        break;
                    case "insert":



                        break;
                    case "update":



                        break;
                    case "delete":



                        break;
                    default:

                        Console.WriteLine($"Команда {command} некорректна");

                        break;
                }
            }

            Console.WriteLine("Для продолжения нажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}
