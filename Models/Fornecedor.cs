using System.ComponentModel.DataAnnotations;

namespace Fornecedores_config
{
    public class Fornecedor
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [MaxLength(14, ErrorMessage = "O campo deve conter {1} caracteres")]
        public double CNPJ { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [MaxLength(45, ErrorMessage = "O campo deve conter {1} caracteres")]
        public string RazaoSocial { get; set; }

        [MaxLength(250, ErrorMessage = "O campo deve conter {1} caracteres")]
        public string Endereco { get; set; }

        [MaxLength(11, ErrorMessage = "O campo deve conter {1} caracteres")]
        public string Fone { get; set; }

        [MaxLength(45, ErrorMessage = "O campo deve conter {1} caracteres")]
        public string Email { get; set; }

        public Fornecedor() { }

    }
}