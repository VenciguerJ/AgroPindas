using System.ComponentModel.DataAnnotations;

namespace Fornecedores_config
{
    public class Fornecedor
    {
        [Required(ErrorMessage = "O campo � obrigat�rio")]
        [RegularExpression(@"^.{1,45}$", ErrorMessage = "O campo deve conter at� 45 caracteres")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo � obrigat�rio")]
        public double CNPJ { get; set; }

        [Required(ErrorMessage = "O campo � obrigat�rio")]
        public string RazaoSocial { get; set;  }

        [Required(ErrorMessage = "O campo � obrigat�rio")]
        public string Endereco { get; set; }

        public string ?Fone { get; set; }

        public string ?Email { get; set; }


        public Fornecedor() { }
    }
}