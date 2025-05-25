using LibraryManagement.Dto.Endereco;
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

        [HttpGet]
        public async Task<ActionResult> Editar(int? id)
        {
            if (id == null)
            {
                TempData["MensagemErro"] = "ID inválido.";
                return RedirectToAction("Index");
            }

            var usuario = await _usuarioRepository.BuscarUsuarioPorId(id.Value);

            if (usuario == null)
            {
                TempData["MensagemErro"] = "Usuário não encontrado.";
                return RedirectToAction("Index");
            }

            var usuarioEditado = new UsuarioEdicaoDto
            {
                Id = usuario.Id,
                NomeCompleto = usuario.NomeCompleto,
                Email = usuario.Email,
                Perfil = usuario.Perfil,
                Turno = usuario.Turno,
                Usuario = usuario.Usuario,
                Endereco = new EnderecoEdicaoDto
                {
                    id = usuario.Endereco.Id,
                    Logradouro = usuario.Endereco.Logradouro,
                    Numero = usuario.Endereco.Numero,
                    Bairro = usuario.Endereco.Bairro,
                    Cidade = usuario.Endereco.Cidade,
                    Estado = usuario.Endereco.Estado,
                    Complemento = usuario.Endereco.Complemento,
                    CEP = usuario.Endereco.CEP,
                    UsuarioId = usuario.Id
                }
            };

            if (usuarioEditado.Perfil == PerfilEnum.Cliente)
            {
                ViewBag.Perfil = PerfilEnum.Cliente;
            }
            else
            {
                ViewBag.Perfil = PerfilEnum.Administrador;
            }

            return View(usuarioEditado);
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

        [HttpPost]
        public async Task<ActionResult> Editar(UsuarioEdicaoDto usuarioEdicaoDto)
        {
            if (ModelState.IsValid)
            {

                var usuario = await _usuarioRepository.Editar(usuarioEdicaoDto);
                TempData["MensagemSucesso"] = "Edição realizada com sucesso.";

                if (usuario.Perfil != PerfilEnum.Cliente)
                {
                    return RedirectToAction("Index", "Funcionario");
                }
                else
                {
                    return RedirectToAction("Index", "Cliente", new {Id = "0"});
                }
            }
            else
            {
                TempData["MensagemErro"] = "Verifique os dados informados.";
                return View(usuarioEdicaoDto);
            }
        }
    }
}
