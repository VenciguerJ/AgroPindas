namespace agropindas.Models;

public class ProducaoViewModel
{
    public SuporteCalha SuporteProducao { get; set; }
    public Producao? ProducaoCalha { get; set; }

    public IEnumerable<Produto> produtosView { get; set; }

    public IEnumerable<Fertilizante>? fertilizantesView { get; set; }

    public LoteMuda? LoteView { get; set; }

    public ProducaoViewModel(SuporteCalha s, IEnumerable<Produto> prod, Producao? p) 
    {
        SuporteProducao = s;
        produtosView = prod;
        ProducaoCalha = p;
    }
    public ProducaoViewModel() { }
}
