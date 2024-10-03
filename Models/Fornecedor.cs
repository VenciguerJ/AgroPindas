using System.ComponentModel.DataAnnotations;

namespace Fornecedores_config
{
    public class Fornecedor
    {
        [Required(ErrorMessage = "O campo é obrigatório")]
        [RegularExpression(@"^.{1,45}$", ErrorMessage = "O campo deve conter até 45 caracteres")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public double CNPJ { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public string RazaoSocial { get; set;  }

        [Required(ErrorMessage = "O campo é obrigatório")]
        public string Endereco { get; set; }

        public string ?Fone { get; set; }

        public string ?Email { get; set; }


        public Fornecedor() { }
    }
}