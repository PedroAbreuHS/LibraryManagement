using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryManagement.Models
{
    public class EnderecoModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Logradouro { get; set; } = string.Empty;

        [Required]
        public string Bairro { get; set; } = string.Empty;

        [Required]
        public string Numero { get; set; } = string.Empty;

        [Required]
        public string Cidade { get; set; } = string.Empty;

        [Required]
        public string Estado { get; set; } = string.Empty;

        [Required]
        public string CEP { get; set; } = string.Empty;
        
        public string? Complemento { get; set; }


        
        public int UsuarioId { get; set; }

        [JsonIgnore]
        public UsuarioModel Usuario { get; set; } = null!;
    }
}
