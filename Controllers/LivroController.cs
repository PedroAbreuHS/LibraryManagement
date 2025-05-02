using LibraryManagement.Dto;
using LibraryManagement.Models;
using LibraryManagement.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class LivroController : Controller
    {
        private readonly ILivroRepository _livroRepository;

        public LivroController(ILivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }


        public async Task<ActionResult<List<LivroModel>>> Index()
        {
            var livros = await _livroRepository.BuscarLivros();
            return View(livros);
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Detalhes(int? id)
        {
            if (id != null)
            {
                var livro = await _livroRepository.BuscarLivroPorId(id.Value);
                return View(livro);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Cadastrar(LivroCriacaoDto livroCriacaoDto, IFormFile foto)
        {
            if (foto != null)
            {
                if (ModelState.IsValid)
                {
                    if (!_livroRepository.VerificaSeJaExisteCadastro(livroCriacaoDto))
                    {
                        TempData["MensagemErro"] = "Código ISBN já cadastrado";
                        return View(livroCriacaoDto);
                    }

                    var livro = await _livroRepository.Cadastrar(livroCriacaoDto, foto);

                    TempData["MensagemSucesso"] = "Livro Cadastrado com Sucesso!";
                    return RedirectToAction("Index");
                }
                TempData["MensagemErro"] = "Verifique os dados preenchidos";
                return View(livroCriacaoDto);
            }
            TempData["MensagemErro"] = "Incluir Imagem de Capa";
            return View(livroCriacaoDto);
        }

        //---
    }
}
