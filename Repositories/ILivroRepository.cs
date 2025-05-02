using LibraryManagement.Dto;
using LibraryManagement.Models;

namespace LibraryManagement.Repositories
{
    public interface ILivroRepository
    {
       Task<List<LivroModel>> BuscarLivros();
       Task<LivroModel> Cadastrar(LivroCriacaoDto livroCriacaoDto, IFormFile foto);
       bool VerificaSeJaExisteCadastro(LivroCriacaoDto livroCriacaoDto);

    }
}
