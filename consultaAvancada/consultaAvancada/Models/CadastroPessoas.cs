namespace consultaAvancada.Models
{
    public class CadastroPessoas
    {
        public string CPF { get; set; }
        public string CNPJ { get; set; }
        public string NomePessoaFisica { get; set; }
        public string NomePessoaJuridica { get; set; }
        public DateTime DataCriacaoCadastro { get; set; }
        public string PaisOrigem { get; set; }
        public string Email { get; set; }
    }
}
