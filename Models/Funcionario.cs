using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace agropindas.Models
{

    public class Funcionario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Preencha o nome")]
        [RegularExpression(@"^.{1,45}$", ErrorMessage = "O campo deve conter apenas letras e números, com até 45 caracteres.")]
        public string ?Nome { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deve ter 11 dígitos.")]
        public string ?CPF { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [RegularExpression(@"^.{1,45}$",ErrorMessage = "A senha deve ter até 45 Caracteres")]
        public string ?Senha { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "O telefone deve ter 11 dígitos")]
        public string ?Fone { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "O e-mail não é válido")]
        public string ?Email { get; set; }

        [Required(ErrorMessage = "O campo é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "O ID do cargo deve ser um número positivo e maior que 0")]
        public int ?IdCargo { get; set; }

        public Funcionario ?Superior { get; set; }

        [DataType(DataType.Date)]
        public DateTime ?DataNascimento { get; set; }

        public void debugger()
        {
            Console.WriteLine($"Nome: {Nome}, Email: {Email}");
            Console.WriteLine($"Senha: {Senha}, CPF: {CPF}");
            Console.WriteLine($"Fone: {Fone}, IdCargo: {IdCargo}");
            Console.WriteLine($"DataNascimento: {DataNascimento}");
            Console.WriteLine($"Id: {Id} IDcargo: {IdCargo}");
            Console.WriteLine($"Superior: {Superior}");
        }

        public Funcionario()
        {

        }
    }
}
