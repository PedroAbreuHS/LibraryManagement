using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Dto.Endereco
{
    public class EnderecoEdicaoDto
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Digite o logradouro completo.")]
        public string Logradouro { get; set; } = string.Empty;

        [Required(ErrorMessage = "Digite o número.")]
        public string Numero { get; set; } = string.Empty;

        [Required(ErrorMessage = "Digite o bairro.")]
        public string Bairro { get; set; } = string.Empty;

        [Required(ErrorMessage = "Digite o cidade.")]
        public string Cidade { get; set; } = string.Empty;

        [Required(ErrorMessage = "Digite o estado.")]
        public string Estado { get; set; } = string.Empty;

        [Required(ErrorMessage = "Digite o CEP.")]
        public string CEP { get; set; } = string.Empty;

        public string? Complemento { get; set; } = string.Empty;

        
        public int UsuarioId { get; set; }
    }
}
