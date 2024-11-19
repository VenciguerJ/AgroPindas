namespace agropindas.Models;

public class PerdaViewModel
{
    public IEnumerable<Lote>? Lotesview { get; set; }
    public IEnumerable<Produto>? ProdutosView { get; set; }

    public Produto? ProdutoPerda { get; set; }

    public Lote? LotePerda { get; set; }

    public PerdaViewModel(IEnumerable<Lote>? lotesview, IEnumerable<Produto>? produtosView)
    {
        Lotesview = lotesview;
        ProdutosView = produtosView;
    }

    public PerdaViewModel() { }
}
