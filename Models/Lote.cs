namespace agropindas.Models;
public class Lote
{
    public int IdCompra { get; set; }
    public int IdProduto { get; set; }

    public Produto? Produto { get; set; }
    public int QuantidadeLote {  get; set; }
    public double? QuantidadeSaida { get; set; }
}
