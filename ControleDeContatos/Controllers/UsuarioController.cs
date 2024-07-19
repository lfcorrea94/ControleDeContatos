using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers
{
    public class UsuarioController : Controller
    {
        // view index Usuario
        public IActionResult Index()
        {
            return View();
        }
    }
}
