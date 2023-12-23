using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Abstractions;
using SegurosPacifico.Models;
using System.Security.Claims;

namespace SegurosPacifico.Controllers
{
    public class UsuariosController : Controller
    {
        private DbContextSalarios dbContext;

        public UsuariosController(DbContextSalarios pContext)
        {
            dbContext = pContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login([Bind] Usuario pUser)
        {
            if (pUser == null)
            {
                return NotFound();
            }
            else  
            {

                var tempUser = dbContext.Usuarios.FirstOrDefault(r => r.Email == pUser.Email);
                if (tempUser == null)
                {
                    return NotFound();
                }
                else
                {
                    if (tempUser.Password.Equals(pUser.Password))
                    {
                        var userClaims = new List<Claim>() { new Claim(ClaimTypes.Name, pUser.Email) };

                        var grandmaIdentity = new ClaimsIdentity(userClaims, "User Identity");
                        var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity });

                        HttpContext.SignInAsync(userPrincipal);

                        return RedirectToAction("Index", "Salarios");
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
