using LibraryManagement.Enums;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class UsuarioModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string NomeCompleto { get; set; } = string.Empty;

        [Required]
        public string Usuario { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public bool Situacao { get; set; } = true;

        [Required]
        public byte[] SenhaHash { get; set; } = null!;

        [Required]
        public byte[] SenhaSalt { get; set; } = null!;

        [Required]
        public PerfilEnum Perfil { get; set; }

        [Required]
        public TurnoEnum Turno { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public DateTime DataAlteracao { get; set; } = DateTime.Now;

        [Required]
        public EnderecoModel Endereco { get; set; } = null!;
    }
}
