namespace LojaOnline.Models
{
    public class Usuario
    {
        public string CpfCnpj { get; set; }  // Chave primária
        public string CodigoProduto { get; set; }  // Chave primária
        public string NomeCompleto { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime? DataSaida { get; set; }  // Nullable, pois no banco permite null
        public string NomeProduto { get; set; }
        public string CpfCnpjBaixa { get; set; }
        public bool Encerrado { get; set; }
    }
}
