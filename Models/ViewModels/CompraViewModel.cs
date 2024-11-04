using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace agropindas.Models;
public class CompraViewModel 
{
    public IEnumerable<Produto>? Produtos { get; set; }
    public IEnumerable<Fornecedor>? Fornecedores { get; set; }

    public Compra? Compra { get; set; }

    [Required]
    public Fornecedor? FornecedorCompra { get; set; }

    public CompraViewModel(IEnumerable<Fornecedor> f, IEnumerable<Produto> p)
    {
        Produtos = p;
        Fornecedores = f;
    }
    public CompraViewModel() { }
}

