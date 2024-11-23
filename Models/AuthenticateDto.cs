using System.ComponentModel.DataAnnotations;

namespace agropindas.Models;

public class AuthenticateDto
{
    [Required(ErrorMessage = "O CPF é obrigatório.")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve ter 11 dígitos.")]
    public string CPF { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória.")]
    [StringLength(20, ErrorMessage = "A senha pode ter no máximo 20 caracteres.")]
    public string Senha { get; set; }
}
