using LibraryManagement.Dto.Usuario;
using LibraryManagement.Models;

namespace LibraryManagement.Repositories
{
    public interface IUsuarioRepository
    {
        Task<List<UsuarioModel>> BuscarUsuarios(int? id);

        Task<UsuarioModel> BuscarUsuarioPorId(int? id);

        Task<bool> VerificaSeUsuarioEEmailExistem(UsuarioCriacaoDto usuarioCriacaoDto);

        Task<UsuarioCriacaoDto> Cadastrar(UsuarioCriacaoDto usuarioCriacaoDto);

        Task<UsuarioModel> Editar(UsuarioEdicaoDto usuarioEdicaoDto);

        Task<UsuarioModel> MudarSituacaoUsuario(int? id);
    }
}
