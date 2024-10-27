using Microsoft.AspNetCore.Mvc;
using LojaOnline.Models;
using System.IO;
using System.Linq;

public class UsuariosController : Controller
{
    private readonly CadastroLojaContext _context;

    public UsuariosController(CadastroLojaContext context)
    {
        _context = context;
    }

    // Método para logar as acoes
    private void LogAction(string message)
    {
        string logFilePath = $@"C:\Login\{DateTime.Now:ddMMyyyy}LojaOnline.txt";
        string logMessage = $"{DateTime.Now}: {message}{Environment.NewLine}";

        // Garante que o diretório exista
        if (!Directory.Exists(@"C:\Login"))
        {
            Directory.CreateDirectory(@"C:\Login");
        }

        // Escreve a mensagem no arquivo de log
        System.IO.File.AppendAllText(logFilePath, logMessage);
    }

    // Método Index atualizado para realizar a pesquisa
    [HttpGet]
    public IActionResult Index(string searchString)
    {
        // Consulta inicial: busca todos os usuários
        var usuarios = from u in _context.Usuarios
                       select u;

        // Verifica se o termo de pesquisa não está vazio
        if (!string.IsNullOrEmpty(searchString))
        {
            // Executa a consulta filtrando por CPF/CNPJ, Nome Completo ou Código do Produto
            usuarios = usuarios.Where(u => u.CpfCnpj.Contains(searchString) ||
                                           u.NomeCompleto.Contains(searchString) ||
                                           u.CodigoProduto.Contains(searchString));
            LogAction($"Consulta realizada: {searchString}");
        }
        else
        {
            LogAction("Usuários foram listados.");
        }

        return View(usuarios.ToList());
    }

    // Captura a navegacao para a pagina de criacao de um novo Usuario
    [HttpGet]
    public IActionResult Create()
    {
        LogAction("Usuario acessou a pagina de criacao de um novo Usuario.");
        return View();
    }

    // Captura a criacao de um novo Usuario
    [HttpPost]
    public IActionResult Create(Usuario usuario)
    {
        // Verificar se o CPF/CNPJ já existe
        var usuarioExistente = _context.Usuarios.FirstOrDefault(u => u.CpfCnpj == usuario.CpfCnpj);

        if (usuarioExistente != null)
        {
            // Se o CPF/CNPJ já existir, exibir uma mensagem de erro
            ModelState.AddModelError("CpfCnpj", "Este CPF/CNPJ ja esta registrado.");
            LogAction($"Tentativa de criar um novo usuario com CPF/CNPJ duplicado: {usuario.CpfCnpj}");
            return View(usuario);
        }

        if (ModelState.IsValid)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            LogAction($"Usuario criado: CPF/CNPJ {usuario.CpfCnpj}, Produto {usuario.CodigoProduto}");
            return RedirectToAction("Index");
        }

        return View(usuario);
    }

    // Captura a navegacao para a pagina de edicao de um Usuario
    [HttpGet]
    public IActionResult Edit(string cpfCnpj, string codigoProduto)
    {
        var usuario = _context.Usuarios.Find(cpfCnpj, codigoProduto);
        if (usuario == null)
        {
            LogAction($"Tentativa de acessar a pagina de atualizacao para um Usuario nao encontrado: CPF/CNPJ {cpfCnpj}, Produto {codigoProduto}");
            return NotFound();
        }

        LogAction($"Usuario acessou a pagina de atualizacao  para CPF/CNPJ {cpfCnpj}, Produto {codigoProduto}");
        return View(usuario);
    }

    // Captura a edicao de um Usuario
    [HttpPost]
    public IActionResult Edit(Usuario usuario)
    {
        if (ModelState.IsValid)
        {
            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
            LogAction($"Usuario atualizado: CPF/CNPJ {usuario.CpfCnpj}, Produto {usuario.CodigoProduto}");
            return RedirectToAction("Index");
        }

        return View(usuario);
    }

    // Captura a navegacao para a pagina de confirmacao de delecao de um Usuario
    [HttpGet]
    public IActionResult Delete(string cpfCnpj, string codigoProduto)
    {
        var usuario = _context.Usuarios.Find(cpfCnpj, codigoProduto);
        if (usuario == null)
        {
            LogAction($"Tentativa de acessar a pagina de delecao para um Usuario nao encontrado: CPF/CNPJ {cpfCnpj}, Produto {codigoProduto}");
            return NotFound();
        }

        LogAction($"Usuario acessou a pagina de confirmacao de delecao para CPF/CNPJ {cpfCnpj}, Produto {codigoProduto}");
        return View(usuario);
    }

    // Captura a delecao de um Usuario
    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(string cpfCnpj, string codigoProduto)
    {
        var usuario = _context.Usuarios.Find(cpfCnpj, codigoProduto);
        if (usuario == null)
        {
            LogAction($"Tentativa de deletar falhou. Usuario nao encontrado: CPF/CNPJ {cpfCnpj}, Produto {codigoProduto}");
            return NotFound();
        }

        _context.Usuarios.Remove(usuario);
        _context.SaveChanges();
        LogAction($"Usuario deletado: CPF/CNPJ {usuario.CpfCnpj}, Produto {usuario.CodigoProduto}");
        return RedirectToAction("Index");
    }
}
