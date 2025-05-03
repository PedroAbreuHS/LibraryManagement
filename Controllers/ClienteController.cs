using LibraryManagement.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IUsuarioRepository _usuario;

        public ClienteController(IUsuarioRepository usuarioRepository)
        {
            _usuario = usuarioRepository;
        }
        public async Task<ActionResult> Index(int? id)
        {
            var clientes = await _usuario.BuscarUsuarios(id);
            return View(clientes);
        }
    }
}
