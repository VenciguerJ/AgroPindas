using System.ComponentModel.DataAnnotations;

namespace agropindas.Models;

public class Cliente
{
    public int Id { get; set; } // IDENTITY indica que é gerado automaticamente

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(50, ErrorMessage = "O nome pode ter no máximo 50 caracteres.")]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [StringLength(50, ErrorMessage = "O e-mail pode ter no máximo 50 caracteres.")]
    [EmailAddress(ErrorMessage = "O e-mail não é válido.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "O CPF é obrigatório.")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve ter 11 dígitos.")]
    public string CPF { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória.")]
    [StringLength(20, ErrorMessage = "A senha pode ter no máximo 20 caracteres.")]
    [DataType(DataType.Password)]
    public string Senha { get; set; }
}