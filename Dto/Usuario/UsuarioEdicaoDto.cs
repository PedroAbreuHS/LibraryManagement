using LibraryManagement.Dto.Endereco;
using LibraryManagement.Enums;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Dto.Usuario
{
    public class UsuarioEdicaoDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome completo.")]
        public string NomeCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "Digite o nome do usuário.")]
        public string Usuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "Digite o email completo.")]
        public string Email { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Selecione um perfi.")]
        public PerfilEnum Perfil { get; set; }

        [Required(ErrorMessage = "Selecione um turno.")]
        public TurnoEnum Turno { get; set; }

        public EnderecoEdicaoDto? Endereco { get; set; }
    }
}
