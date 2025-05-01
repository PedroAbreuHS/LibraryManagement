using LibraryManagement.Models;

namespace LibraryManagement.Repositories
{
    public interface ILivroRepository
    {
       Task<List<LivroModel>> BuscarLivros();
    }
}
