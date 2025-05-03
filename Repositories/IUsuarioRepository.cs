using LibraryManagement.Models;

namespace LibraryManagement.Repositories
{
    public interface IUsuarioRepository
    {
        Task<List<UsuarioModel>> BuscarUsuarios(int? id);
    }
}
