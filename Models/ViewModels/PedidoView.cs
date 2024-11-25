namespace agropindas.Models;

public class PedidoView
{
    public IEnumerable<PedidoDto> Pedido { get; set; }
    public IEnumerable<Cliente> Clientes { get; set; }
    public PedidoView(IEnumerable<PedidoDto> p , IEnumerable<Cliente> c)
    {
        Pedido = p;
        Clientes = c;
    }
}

