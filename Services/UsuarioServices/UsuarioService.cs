using LibraryManagement.Data;
using LibraryManagement.Models;
using LibraryManagement.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Services.UsuarioServices
{
    public class UsuarioService : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UsuarioModel>> BuscarUsuarios(int? id)
        {
            var registros = new List<UsuarioModel>();

            if (id != null)
            {
                registros = await _context.Usuarios.Where(cliente => cliente.Perfil == 0)
                    .Include(endereco => endereco.Endereco).ToListAsync();
            }

            registros = await _context.Usuarios.Where(funcionario => funcionario.Perfil != 0)
                    .Include(endereco => endereco.Endereco).ToListAsync();

            return registros;
        }
    }
}
