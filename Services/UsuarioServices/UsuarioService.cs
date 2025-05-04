using LibraryManagement.Data;
using LibraryManagement.Dto.Usuario;
using LibraryManagement.Enums;
using LibraryManagement.Models;
using LibraryManagement.Repositories;
using LibraryManagement.Services.Autenticacao;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Services.UsuarioServices
{
    public class UsuarioService : IUsuarioRepository
    {
        private readonly AppDbContext _context;
        private readonly IAutenticacaoRepository _autenticacao;

        public UsuarioService(AppDbContext context, IAutenticacaoRepository autenticacao)
        {
            _context = context;
            _autenticacao = autenticacao;
        }

        public async Task<List<UsuarioModel>> BuscarUsuarios(int? id)
        {            
            // id == 0 → clientes
            // id == 1 ou null → funcionários
            if (id == 0)
            {
                return await _context.Usuarios
                    .Where(u => u.Perfil == PerfilEnum.Cliente)
                    .Include(u => u.Endereco)
                    .ToListAsync();
            }
            else
            {
                return await _context.Usuarios
                    .Where(u => u.Perfil != PerfilEnum.Cliente)
                    .Include(u => u.Endereco)
                    .ToListAsync();
            }
        }



        public async Task<UsuarioCriacaoDto> Cadastrar(UsuarioCriacaoDto usuarioCriacaoDto)
        {
            _autenticacao.CriarSenhaHash(usuarioCriacaoDto.Senha, out byte[] senhaHash, out byte[] senhaSalt);

            var enderecoUsuarioNovo = new EnderecoModel
            {
                Logradouro = usuarioCriacaoDto.Logradouro,
                Numero = usuarioCriacaoDto.Numero,
                Bairro = usuarioCriacaoDto.Bairro,
                Cidade = usuarioCriacaoDto.Cidade,
                Estado = usuarioCriacaoDto.Estado,
                CEP = usuarioCriacaoDto.CEP,
                Complemento = usuarioCriacaoDto.Complemento
            };

            var usuarioNovo = new UsuarioModel
            {
                NomeCompleto = usuarioCriacaoDto.NomeCompleto,
                Usuario = usuarioCriacaoDto.Usuario,
                Email = usuarioCriacaoDto.Email,
                Perfil = usuarioCriacaoDto.Perfil,
                Turno = usuarioCriacaoDto.Turno,
                SenhaHash = senhaHash,
                SenhaSalt = senhaSalt,
                Endereco = enderecoUsuarioNovo
            };

            _context.Usuarios.Add(usuarioNovo);
            await _context.SaveChangesAsync();

            return usuarioCriacaoDto;
        }




        public async Task<bool> VerificaSeUsuarioEEmailExistem(UsuarioCriacaoDto usuarioCriacaoDto)
        {
            var usuarioBanco = await _context.Usuarios
                .FirstOrDefaultAsync(usuario => usuario.Email == usuarioCriacaoDto.Email || 
                                     usuario.Usuario == usuarioCriacaoDto.Usuario);

            if (usuarioBanco == null)
            {
                return true;
            }

            return false;
        }
    }
}
