namespace agropindas.Models;
public class CompraViewModel 
{
    public IEnumerable<Produto>? Produtos { get; set; }
    public IEnumerable<Fornecedor>? Fornecedores { get; set; }
    public Compra? Compra { get; set; }

    public Fornecedor? FornecedorCompra { get; set; }

    public CompraViewModel(IEnumerable<Fornecedor> f, IEnumerable<Produto> p)
    {
        Produtos = p;
        Fornecedores = f;
    }
    public CompraViewModel() { }
}

