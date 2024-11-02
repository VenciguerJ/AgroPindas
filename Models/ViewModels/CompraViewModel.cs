namespace agropindas.Models;
public class CompraViewModel 
{
    public IEnumerable<Produto>? Produtos { get; set; }
    public IEnumerable<Fornecedor>? Fornecedores { get; set; }
    public Compra? Compra { get; set; }


    public Fornecedor? FornecedorCompra { get; set; }
    public List<Produto>? ProdutosCompra { get; set; }

    public List<Lote>? Lotes { get; set; }

    public CompraViewModel(IEnumerable<Fornecedor> f, IEnumerable<Produto> p, List<Lote> lotes)
    {
        Produtos = p;
        Fornecedores = f;
        Lotes = lotes;
    }
}

