using System.Data.SqlClient;

namespace MonitoramentoApp.Controllers.Monitoramento
{
    public class RemoveFila
    {
        private readonly string _connectionString;

        public RemoveFila(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Remover(int idExecucao)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "UPDATE TBy9_LogExecucao SET Status = 'Removido', Observacao = 'Retirado da fila manualmente' WHERE ID_Execucao = @idExecucao";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idExecucao", idExecucao);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
