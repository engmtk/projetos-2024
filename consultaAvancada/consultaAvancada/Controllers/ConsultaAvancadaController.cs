using Microsoft.AspNetCore.Mvc;
using consultaAvancada.Models;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace consultaAvancada.Controllers
{
    public class ConsultaAvancadaController : Controller
    {
        private readonly string _connectionString = "Data Source = ALEXANDRE\\SQLEXPRESS; Initial Catalog = CadastroDB; User ID = prd_1; Password = Cebol@24";

        // Método para carregar a página inicial vazia
        [HttpGet] // Carrega a página vazia sem dados inicialmente
        public IActionResult ConsultaAvancada()
        {
            return View(); // Retorna a view vazia, sem dados
        }

        // Método para consulta avançada ao clicar no botão
        [HttpPost] // Altera para HttpPost para execução da consulta
        public IActionResult ExecutarConsultaAvancada()
        {
            List<CadastroPessoas> listaPessoas = ObterListaPessoas(); // Obtém os dados da consulta
            return View("ConsultaAvancada", listaPessoas); // Retorna a view com os dados preenchidos
        }

        // Método para exportar os dados para Excel
        [HttpPost] // Exporta após a execução da consulta
        public IActionResult ExportarParaExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Define o contexto de licença

            var listaPessoas = ObterListaPessoas(); // Obter os dados da consulta

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("ConsultaAvancada");

                // Cabeçalhos
                worksheet.Cells[1, 1].Value = "CPF";
                worksheet.Cells[1, 2].Value = "CNPJ";
                worksheet.Cells[1, 3].Value = "Nome Pessoa Física";
                worksheet.Cells[1, 4].Value = "Nome Pessoa Jurídica";
                worksheet.Cells[1, 5].Value = "Data de Criação";
                worksheet.Cells[1, 6].Value = "País de Origem";
                worksheet.Cells[1, 7].Value = "Email";

                // Preencher os dados
                for (int i = 0; i < listaPessoas.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = listaPessoas[i].CPF;
                    worksheet.Cells[i + 2, 2].Value = listaPessoas[i].CNPJ;
                    worksheet.Cells[i + 2, 3].Value = listaPessoas[i].NomePessoaFisica;
                    worksheet.Cells[i + 2, 4].Value = listaPessoas[i].NomePessoaJuridica;
                    worksheet.Cells[i + 2, 5].Value = listaPessoas[i].DataCriacaoCadastro.ToString("dd/MM/yyyy");
                    worksheet.Cells[i + 2, 6].Value = listaPessoas[i].PaisOrigem;
                    worksheet.Cells[i + 2, 7].Value = listaPessoas[i].Email;
                }

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                string excelName = $"ConsultaAvancada-{DateTime.Now:yyyyMMddHHmmss}.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
        }

        // Método para obter dados da consulta avançada
        private List<CadastroPessoas> ObterListaPessoas()
        {
            List<CadastroPessoas> listaPessoas = new List<CadastroPessoas>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("spConsultaAvancada", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    listaPessoas.Add(new CadastroPessoas
                    {
                        CPF = reader["CPF"].ToString(),
                        CNPJ = reader["CNPJ"].ToString(),
                        NomePessoaFisica = reader["NomePessoaFisica"].ToString(),
                        NomePessoaJuridica = reader["NomePessoaJuridica"].ToString(),
                        DataCriacaoCadastro = (DateTime)reader["DataCriacaoCadastro"],
                        PaisOrigem = reader["PaisOrigem"].ToString(),
                        Email = reader["Email"].ToString()
                    });
                }
            }

            return listaPessoas;
        }
    }
}
