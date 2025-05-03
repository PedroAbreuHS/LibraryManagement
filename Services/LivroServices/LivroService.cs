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
        private readonly string _nomeServidor;

        public LivroService(AppDbContext context, IWebHostEnvironment sistema)
        {
            _context = context;
            _nomeServidor = sistema.WebRootPath;
        }

        public async Task<LivroModel> BuscarLivroPorId(int? id)
        {
            var livroBanco = await _context.Livros.FirstOrDefaultAsync(x => x.Id == id);
            return livroBanco;
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
            var nomeCaminho = await GerarCaminhoDeArquivo(foto);

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

        public async Task<LivroModel?> Editar(LivroEdicaoDto livroEdicaoDto, IFormFile foto)
        {
            var livroExistente = await _context.Livros.FirstOrDefaultAsync(l => l.Id == livroEdicaoDto.Id);
            if (livroExistente == null)
            {
                return null;
            }

            var nomeCaminho = "";

            if (foto != null)
            {
                string caminhoCapaExistente = Path.Combine(_nomeServidor, "Imagem", livroExistente.Capa);
                if (File.Exists(caminhoCapaExistente))
                {
                    File.Delete(caminhoCapaExistente);    
                }
                
                nomeCaminho = await GerarCaminhoDeArquivo(foto);                
            }

            // Atualiza os campos
            livroExistente.Titulo = livroEdicaoDto.Titulo;
            livroExistente.Autor = livroEdicaoDto.Autor;
            livroExistente.Descricao = livroEdicaoDto.Descricao;
            livroExistente.QuantidadeEmEstoque = livroEdicaoDto.QuantidadeEmEstoque;
            livroExistente.AnoPublicacao = livroEdicaoDto.AnoPublicacao;
            livroExistente.ISBN = livroEdicaoDto.ISBN;
            livroExistente.Genero = livroEdicaoDto.Genero;
            livroExistente.DataDeAlteracao = DateTime.Now;

            if (nomeCaminho != "")
            {
                livroExistente.Capa = nomeCaminho;
            }            

            _context.Livros.Update(livroExistente);
            await _context.SaveChangesAsync();

            return livroExistente;
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

        public async Task<string> GerarCaminhoDeArquivo(IFormFile foto)
        {
            var extensao = Path.GetExtension(foto.FileName).ToLower();
            var nomeCaminho = Guid.NewGuid().ToString() + extensao;
            string caminhoParaSalvarImagens = Path.Combine(_nomeServidor, "Imagem");

            if (!Directory.Exists(caminhoParaSalvarImagens))
            {
                Directory.CreateDirectory(caminhoParaSalvarImagens);
            }

            string caminhoCompleto = Path.Combine(caminhoParaSalvarImagens, nomeCaminho);

            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                await foto.CopyToAsync(stream);
            }

            return nomeCaminho;
        }

    }

}
