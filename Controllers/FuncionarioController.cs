using LibraryManagement.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class FuncionarioController : Controller
    {
        private readonly IUsuarioRepository _usuario;

        public FuncionarioController(IUsuarioRepository usuario)
        {
            _usuario = usuario;
        }

        public async Task<ActionResult> Index()
        {
            var funcionarios = await _usuario.BuscarUsuarios(null);
            return View(funcionarios);
        }
    }
}
