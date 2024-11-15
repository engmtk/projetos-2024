using System;
using System.Data.SqlClient;

namespace ExecutionsRobot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Iniciando o robô de execução...");

            // Laço para rodar o robô a cada 30 segundos
            while (true)
            {
                InvocarProcedure();
                System.Threading.Thread.Sleep(30000); // Intervalo de 30 segundos
            }
        }

        static void InvocarProcedure()
        {
            // String de conexão ao banco de dados
            string connectionString = "Data Source = ALEXANDRE\\SQLEXPRESS; Initial Catalog = CadastroDB; User ID = prd_1; Password = Cebol@24";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("PRY9_VERIFICAPENDENCIA", conn);  // Nome da sua procedure
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    // Executar a procedure
                    cmd.ExecuteNonQuery();

                    Console.WriteLine("Procedure executada com sucesso em " + DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao executar a procedure: {ex.Message}");
            }
        }
    }
}
