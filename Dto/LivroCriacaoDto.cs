using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Dto
{
    public class LivroCriacaoDto
    {
        [Required(ErrorMessage = "Insira um título!")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Insira uma descrição!")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "Insira uma imagem para capa!")]
        public string Capa { get; set; } = string.Empty;

        [Required(ErrorMessage = "Insira um código ISBN!")]
        public string ISBN { get; set; } = string.Empty;

        [Required(ErrorMessage = "Insira o autor!")]
        public string Autor { get; set; } = string.Empty;

        [Required(ErrorMessage = "Insira o genero!")]
        public string Genero { get; set; } = string.Empty;

        [Required(ErrorMessage = "Insira o ano de publicação do livro!")]
        public int AnoPublicacao { get; set; }

        [Required(ErrorMessage = "Informe a quantidade do estoque!")]
        public int QuantidadeEmEstoque { get; set; }
    }
}
