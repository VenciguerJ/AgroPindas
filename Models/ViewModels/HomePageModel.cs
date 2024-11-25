using Microsoft.Identity.Client;

namespace agropindas.Models.ViewModels
{
    public class HomePageModel
    {
        public int TotalProdutos { get; set; }
        public int TotalClientes { get; set; }
        public int TotalProducoes { get; set; }
        public int ProducoesConcluidas { get; set; }
        public int ProducoesPendentes { get; set; }
        public int TotalPedidos { get; set; }
    }
}
