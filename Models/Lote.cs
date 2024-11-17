namespace agropindas.Models;
using System.Text.Json.Serialization;

public class Lote
{
    public int IdCompra { get; set; }
    public int IdProduto { get; set; }

    [JsonIgnore]
    public Produto? Produto { get; set; }
    public int QuantidadeLote {  get; set; }
    public int QuantidadeSaida { get; set; }
}
