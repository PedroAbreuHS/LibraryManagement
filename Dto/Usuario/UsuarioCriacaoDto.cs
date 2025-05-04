using LibraryManagement.Enums;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Dto.Usuario
{
    public class UsuarioCriacaoDto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o Nome Completo")]
        public string NomeCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "Digite o Usuário")]
        public string Usuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "Digite o E-mail")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Digite situação")]
        public bool Situacao { get; set; } = true;

        [Required(ErrorMessage = "Digite a Senha")]
        [MinLength(6, ErrorMessage = "A senha precisa conter no mínimo 6 caracteres")]
        public string Senha { get; set; } = null!;

        [Required(ErrorMessage = "Digite a Confirmação de Senha")]
        [Compare(nameof(Senha), ErrorMessage = "As senhas não coincidem")]
        public string ConfirmarSenha { get; set; } = null!;

        
        public PerfilEnum Perfil { get; set; }

        
        public TurnoEnum Turno { get; set; }

        [Required(ErrorMessage = "Digite o Logradouro")]
        public string Logradouro { get; set; } = string.Empty;

        [Required(ErrorMessage = "Digite o Bairro")]
        public string Bairro { get; set; } = string.Empty;

        [Required(ErrorMessage = "Digite o Número")]
        public string Numero { get; set; } = string.Empty;

        [Required(ErrorMessage = "Digite a Cidade")]
        public string Cidade { get; set; } = string.Empty;

        [Required(ErrorMessage = "Digite o Estado")]
        public string Estado { get; set; } = string.Empty;

        [Required(ErrorMessage = "Digite o CEP")]
        public string CEP { get; set; } = string.Empty;

        public string? Complemento { get; set; }
    }
}
