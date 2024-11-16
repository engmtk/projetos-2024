using System.Data.SqlClient;

namespace MonitoramentoApp.Controllers.Monitoramento
{
    public class ReiniciaFluxo
    {
        private readonly string _connectionString;

        public ReiniciaFluxo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Reiniciar(int idExecucao)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "UPDATE TBy9_LogExecucao SET Status = 'Aguardando Execução', Observacao = 'Reiniciado manualmente' WHERE ID_Execucao = @idExecucao";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idExecucao", idExecucao);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
