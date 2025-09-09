using Microsoft.AspNetCore.Mvc;
using Sistema_de_Gestão_de_Estoque.data;
using Sistema_de_Gestão_de_Estoque.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Sistema_de_Gestão_de_Estoque.Controllers
{
    public class AccountsController : Controller
    {
        private readonly BancoContext _Context;

        public AccountsController(BancoContext context)
        {
            _Context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Usuario model)
        {
            //Salvar usuario no banco
            if (ModelState.IsValid)
            {
                _Context.Usuarios.Add(model);
                _Context.SaveChanges();
                //Redirecionar para a página de login
                return RedirectToAction("Login");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string senha)
        {
            //Busca se o usuario existe no banco
            var usuario = _Context.Usuarios.FirstOrDefault(u => u.Email == email && u.Senha == senha); 

            if (usuario != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Name, usuario.Nome), // 👈 Isso vai para User.Identity.Name
                    new Claim(ClaimTypes.Email, usuario.Email)
                };
                
                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true, // mantém logado mesmo fechando navegador
                        ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1) // expira em 1h
                    };

                    // Faz o login com cookie
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);



                TempData["Mensagem"] = "Login realizado com sucesso!";
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMessage = "Usuário ou senha inválidos.";
            return View();
        }

        public IActionResult Perfil()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var usuario = _Context.Usuarios.FirstOrDefault(u => u.Id == userId);

            if (usuario == null) return NotFound();

            return View(usuario);
        }

        public IActionResult Logout()
        {

            //Desloga o usuario
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            //retorna para a página de login
            return RedirectToAction("Login", "Accounts");
        }

    }
}
