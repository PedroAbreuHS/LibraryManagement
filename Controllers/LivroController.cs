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
    }
}
