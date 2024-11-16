using Microsoft.AspNetCore.Mvc;
using MonitoramentoApp.Models;
using MonitoramentoApp.Services;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using MonitoramentoApp.Controllers.Monitoramento;

public class MonitoramentoController : Controller
{
    private readonly string _connectionString = "Data Source = ALEXANDRE\\SQLEXPRESS; Initial Catalog = CadastroDB; User ID = prd_1; Password = Cebol@24";
    private readonly LogService _logService;

    public MonitoramentoController()
    {
        _logService = new LogService();
    }

    public IActionResult Index(int page = 1)
    {
        int pageSize = 20;
        List<LogExecucao> logs = new List<LogExecucao>();

        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            conn.Open();
            string query = "SELECT * FROM TBy9_LogExecucao ORDER BY DataInicio DESC";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                logs.Add(new LogExecucao
                {
                    ID_Execucao = (int)reader["ID_Execucao"],
                    NomeProcedure = reader["NomeProcedure"].ToString(),
                    DataInicio = (DateTime)reader["DataInicio"],
                    DataFim = reader["DataFim"] as DateTime?,
                    Status = reader["Status"].ToString(),
                    Duracao = reader["Duracao"] as int?,
                    Observacao = reader["Observacao"].ToString()
                });
            }
        }

        var totalLogs = logs.Count;
        var logsPaginados = logs.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        ViewBag.TotalPages = (int)Math.Ceiling((double)totalLogs / pageSize);
        ViewBag.CurrentPage = page;

        return View(logsPaginados);
    }

    // Método para retirar da fila e atualizar o status e observação
    [HttpPost]
    [Route("Monitoramento/RetirarDaFila/{idExecucao}")]
    public IActionResult RetirarDaFila(int idExecucao)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            conn.Open();
            string query = "UPDATE TBy9_LogExecucao SET Status = 'Removido', Observacao = 'Removido' WHERE ID_Execucao = @idExecucao";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@idExecucao", idExecucao);
            cmd.ExecuteNonQuery();
        }

        _logService.EscreverLog($"Execução {idExecucao} foi retirada da fila manualmente.");
        return RedirectToAction("Index");
    }

    // Método para retornar uma execução removida para a fila
    [HttpPost]
    [Route("Monitoramento/RetornarParaFila/{idExecucao}")]
    public IActionResult RetornarParaFila(int idExecucao)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            conn.Open();
            string query = "UPDATE TBy9_LogExecucao SET Status = 'Em Fila', Observacao = 'ID_Execução que foi removido, está sendo processado a partir de agora.' WHERE ID_Execucao = @idExecucao";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@idExecucao", idExecucao);
            cmd.ExecuteNonQuery();
        }

        _logService.EscreverLog($"Execução {idExecucao} foi retornada para a fila manualmente.");
        return RedirectToAction("Index");
    }

    // Método para reiniciar o fluxo e atualizar a observação
    [HttpPost]
    [Route("Monitoramento/ReiniciarFluxo/{idExecucao}")]
    public IActionResult ReiniciarFluxo(int idExecucao)
    {
        ReiniciaFluxo reiniciaFluxo = new ReiniciaFluxo(_connectionString);
        reiniciaFluxo.Reiniciar(idExecucao);

        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            conn.Open();
            string query = "UPDATE TBy9_LogExecucao SET Observacao = 'Fluxo reiniciado manualmente' WHERE ID_Execucao = @idExecucao";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@idExecucao", idExecucao);
            cmd.ExecuteNonQuery();
        }

        _logService.EscreverLog($"Execução {idExecucao} foi reiniciada manualmente.");
        return RedirectToAction("Index");
    }

    // Método para deletar uma execução
    [HttpPost]
    [Route("Monitoramento/DeletarExecucao/{idExecucao}")]
    public IActionResult DeletarExecucao(int idExecucao)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            conn.Open();
            string query = "DELETE FROM TBy9_LogExecucao WHERE ID_Execucao = @idExecucao";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@idExecucao", idExecucao);
            cmd.ExecuteNonQuery();
        }

        _logService.EscreverLog($"Execução {idExecucao} foi deletada.");
        return RedirectToAction("Index");
    }
}
