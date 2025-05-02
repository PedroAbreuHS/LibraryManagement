using LibraryManagement.Data;
using LibraryManagement.Dto;
using LibraryManagement.Models;
using LibraryManagement.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace LibraryManagement.Services.LivroServices
{
    public class LivroService : ILivroRepository
    {
        private readonly AppDbContext _context;
        private string _nomeServidor;

        public LivroService(AppDbContext context, IWebHostEnvironment sistema)
        {
            _context = context;
            _nomeServidor = sistema.WebRootPath;
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

        public async Task<LivroModel> Cadastrar(LivroCriacaoDto livroCriacaoDto, IFormFile foto)
        {
            var codigoUnico = Guid.NewGuid().ToString();
            var nomeCaminho = foto.FileName.Replace(" ", "").ToLower() + codigoUnico + livroCriacaoDto.ISBN + ".PNG";
            string caminhoParaSalvarImagens = _nomeServidor + "\\Imagem\\";
            

            if (!Directory.Exists(caminhoParaSalvarImagens))
            {
                Directory.CreateDirectory(caminhoParaSalvarImagens);
            }

            using (var stream = System.IO.File.Create(caminhoParaSalvarImagens + nomeCaminho))
            {
                foto.CopyToAsync(stream).Wait();
            }

            var livro = new LivroModel
            {
                Titulo = livroCriacaoDto.Titulo,
                Capa = nomeCaminho,
                Autor = livroCriacaoDto.Autor,
                Descricao = livroCriacaoDto.Descricao,
                QuantidadeEmEstoque = livroCriacaoDto.QuantidadeEmEstoque,
                AnoPublicacao = livroCriacaoDto.AnoPublicacao,
                ISBN = livroCriacaoDto.ISBN,
                Genero = livroCriacaoDto.Genero
            };

            await _context.Livros.AddAsync(livro);
            await _context.SaveChangesAsync();

            return livro;
        }

        public bool VerificaSeJaExisteCadastro(LivroCriacaoDto livroCriacaoDto)
        {
            var livroBanco = _context.Livros.FirstOrDefault(l => l.ISBN == livroCriacaoDto.ISBN);

            if (livroBanco != null)
            {
                return false;
            }

            return true;
        }
    }
}
