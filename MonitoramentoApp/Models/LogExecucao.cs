namespace MonitoramentoApp.Models
{
    public class LogExecucao
    {
        public int ID_Execucao { get; set; }
        public string NomeProcedure { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public string Status { get; set; }
        public int? Duracao { get; set; }
        public string Observacao { get; set; }
    }
}
