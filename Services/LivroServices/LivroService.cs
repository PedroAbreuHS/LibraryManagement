using LibraryManagement.Data;
using LibraryManagement.Models;
using LibraryManagement.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace LibraryManagement.Services.LivroServices
{
    public class LivroService : ILivroRepository
    {
        private readonly AppDbContext _context;

        public LivroService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<LivroModel>> BuscarLivros()
        {
            try
            {

                var livros = await _context.Livros.ToListAsync();
                return livros;
            }
            catch(DbException dbEx)
            {
                throw new ApplicationException("Erro ao acessar o banco de dados para buscar livros.", dbEx);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro inesperado ao buscar livros.", ex);
            }
        }


    }
}
