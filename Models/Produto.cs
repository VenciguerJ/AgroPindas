using System;
using System.ComponentModel.DataAnnotations;

namespace agropindas.Models;

public class Produto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Campo Obrigatório")]
    [MaxLength(45)]
    public string Nome { get; set; }

    [StringLength(90)]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "Campo Obrigatório")]
    public int TemperaturaPlantio { get; set; }

    [Required(ErrorMessage = "Campo Obrigatório")]
    public int DiasColheita { get; set; }

    [Required(ErrorMessage = "Campo Obrigatório")]
    public int UnidadeCadastro { get; set; }
    public ProdAssets? OUnidadeCadastro { get; set; }

    [Required(ErrorMessage = "Campo Obrigatório")]
    public int TipoProduto { get; set; }
    public ProdAssets? OTipoProduto { get; set; }

    public decimal ValorProduto { get; set; }

    // Função Debugger
    public void Debugger()
    {
        Console.WriteLine("Debugging Produto:");
        Console.WriteLine($"Id: {Id}");
        Console.WriteLine($"Nome: {Nome}");
        Console.WriteLine($"Descrição: {Descricao}");
        Console.WriteLine($"Temperatura de Plantio: {TemperaturaPlantio}");
        Console.WriteLine($"Dias para Colheita: {DiasColheita}");

        if (OUnidadeCadastro != null)
        {
            Console.WriteLine("Unidade de Cadastro:");
            Console.WriteLine($"   Id: {OUnidadeCadastro.Id}");
            Console.WriteLine($"   Nome: {OUnidadeCadastro.Nome}");
        }
        else
        {
            Console.WriteLine("Unidade de Cadastro: Nulo");
        }

        if (OTipoProduto != null)
        {
            Console.WriteLine("Tipo de Produto:");
            Console.WriteLine($"   Id: {OTipoProduto.Id}");
            Console.WriteLine($"   Nome: {OTipoProduto.Nome}");
        }
        else
        {
            Console.WriteLine("Tipo de Produto: Nulo");
        }
    }
}


