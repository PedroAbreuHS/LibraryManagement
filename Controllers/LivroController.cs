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

        [HttpPost]
        public async Task<ActionResult> Cadastrar(LivroCriacaoDto livroCriacaoDto, IFormFile foto)
        {
            if (foto != null)
            {
                if (ModelState.IsValid)
                {
                    if (!_livroRepository.VerificaSeJaExisteCadastro(livroCriacaoDto))
                    {
                        return View(livroCriacaoDto);
                    }

                    var livro = await _livroRepository.Cadastrar(livroCriacaoDto, foto);

                    return RedirectToAction("Index");
                }
                return View(livroCriacaoDto);
            }
            return View(livroCriacaoDto);
        }
    }
}
