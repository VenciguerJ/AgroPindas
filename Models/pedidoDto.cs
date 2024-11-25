namespace agropindas.Models;

public class PedidoDto
{
    public int Id { get; set; }
    public string CPF { get; set; }
    public string Produtos { get; set; }
    public DateTime DataPedido { get; set; } 
}
