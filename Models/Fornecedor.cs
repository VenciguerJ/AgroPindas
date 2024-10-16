using System.ComponentModel.DataAnnotations;
using System;
using System.Text.RegularExpressions;
namespace agropindas.Models;

public class Fornecedor
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Campo Obrigatório")]
    [MaxLength(14, ErrorMessage = "O campo deve conter {1} caracteres")]
    public String CNPJ { get; set; }

    [Required(ErrorMessage = "Campo Obrigatório")]
    [MaxLength(45, ErrorMessage = "O campo deve conter {1} caracteres")]
    public string RazaoSocial { get; set; }

    [MaxLength(250, ErrorMessage = "O campo deve conter {1} caracteres")]
    public string Endereco { get; set; }

    [MaxLength(11, ErrorMessage = "O campo deve conter {1} caracteres")]
    public string Fone { get; set; }

    [MaxLength(45, ErrorMessage = "O campo deve conter {1} caracteres")]
    public string Email { get; set; }

    public void Debugger()
    {
        Console.WriteLine($"ID: {Id}");
        Console.WriteLine($"CNPJ: {CNPJ}");
        Console.WriteLine($"Razão Social: {RazaoSocial}");
        Console.WriteLine($"Endereço: {Endereco}");
        Console.WriteLine($"Telefone: {Fone}");
        Console.WriteLine($"Email: {Email}");
    }

    public Fornecedor() { }

}