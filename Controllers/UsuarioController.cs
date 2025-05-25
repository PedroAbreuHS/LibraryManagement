using LibraryManagement.Dto.Usuario;
using LibraryManagement.Enums;
using LibraryManagement.Models;
using LibraryManagement.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<ActionResult> Index(int? id)
        {
            var usuarios = await _usuarioRepository.BuscarUsuarios(id);

            if (id == 0)
            {
                return View("Clientes", usuarios); // view Clientes.cshtml
            }

            return View("Funcionarios", usuarios); // view Funcionarios.cshtml
        }


        [HttpGet]
        public ActionResult Cadastrar(int? id)
        {
            ViewBag.Perfil = PerfilEnum.Administrador;

            if (id != null)
            {
                ViewBag.Perfil = PerfilEnum.Cliente;
            }

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Detalhes(int? id)
        {
            if (id != null)
            {
                var usuario = await _usuarioRepository.BuscarUsuarioPorId(id);
                return View(usuario);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Cadastrar(UsuarioCriacaoDto usuarioCriacao)
        {
            foreach (var modelState in ModelState)
            {
                foreach (var error in modelState.Value.Errors)
                {
                    Console.WriteLine($"Erro em {modelState.Key}: {error.ErrorMessage}");
                }
            }

            if (ModelState.IsValid)
            {
                if (!await _usuarioRepository.VerificaSeUsuarioEEmailExistem(usuarioCriacao))
                {
                    TempData["MensagemErro"] = "Já existe email/usuario cadastrado!";
                    return View(usuarioCriacao);
                }

                //cadastrar
                var usuarioCadastro = await _usuarioRepository.Cadastrar(usuarioCriacao);

                TempData["MensagemSucesso"] = "Usuário Cadastrado Com Sucesso!";
                if (usuarioCadastro.Perfil != PerfilEnum.Cliente)
                {
                    return RedirectToAction("Index", "Funcionario");
                }
                return RedirectToAction("Index", "Cliente", new {Id = 0});
            }
            TempData["MensagemErro"] = "Verifique os dados informados";
            return View(usuarioCriacao);
        }

        [HttpPost]
        public async Task<ActionResult> MudarSituacaoUsuario(UsuarioModel usuarioModel)
        {

            if (usuarioModel.Id != 0 && usuarioModel.Id != null)
            {
                var usuarioExistente = await _usuarioRepository.MudarSituacaoUsuario(usuarioModel.Id);

                if (usuarioExistente.Situacao)
                {
                    TempData["MensagemSucesso"] = "Usuário Ativo com Sucesso!";
                }
                TempData["MensagemSucesso"] = "Usuário Inativado com Sucesso!";
                

                if (usuarioExistente.Perfil != PerfilEnum.Cliente)
                {
                    return RedirectToAction("Index", "Funcionario");
                }
                return RedirectToAction("Index", "Cliente", new { Id = 0 });
            }

            return RedirectToAction("Index");
        }


    }
}
