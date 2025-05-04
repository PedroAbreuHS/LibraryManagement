using LibraryManagement.Dto.Livro;
using LibraryManagement.Models;

namespace LibraryManagement.Repositories
{
    public interface ILivroRepository
    {
       Task<List<LivroModel>> BuscarLivros();
       Task<LivroModel> Cadastrar(LivroCriacaoDto livroCriacaoDto, IFormFile foto);
       bool VerificaSeJaExisteCadastro(LivroCriacaoDto livroCriacaoDto);
       Task<LivroModel> BuscarLivroPorId(int? id);
       Task<LivroModel> Editar(LivroEdicaoDto livroEdicaoDto, IFormFile foto);
    }
}
