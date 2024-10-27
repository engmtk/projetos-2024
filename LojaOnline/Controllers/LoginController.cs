using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

public class LoginController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View(); // Exibe a view de login
    }

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        // Autenticação simples
        if (username == "admin" && password == "123456")
        {
            // Salva o usuário na sessão
            HttpContext.Session.SetString("user", "admin");
            return RedirectToAction("Index", "Usuarios"); // Redireciona para a página principal após login
        }

        ViewBag.ErrorMessage = "Usuário ou senha incorretos";
        return View("Index"); // Retorna ao login se falhar
    }

    [HttpPost]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear(); // Limpa a sessão ao sair
        return RedirectToAction("Index", "Login");
    }
}
