namespace agropindas.Models;

public class ProducaoViewModel
{
    public SuporteCalha SuporteProducao { get; set; }
    public Producao? ProducaoCalha { get; set; }

    public IEnumerable<Produto> produtosView { get; set; }
    public Lote? LoteView {  get; set; }
    public Fertilizante? FertilizanteView { get; set; }
    public ProducaoViewModel(SuporteCalha s, IEnumerable<Produto> prod) 
    {
        SuporteProducao = s;
        produtosView = prod;
    }
    public ProducaoViewModel() { }
}
