namespace agropindas.Models;
public class Lote
{
    public int IdCompra { get; set; }
    public int IdProduto { get; set; }

    public Produto? Produto { get; set; }
    public int QuantidadeLote 
    {
        get { return QuantidadeLote; } 
        set
        {
            if(value < 0)
            {
                throw new ArgumentException("Não é possível ter quantidade negativa em estoque!");
            }
        } 
    }
    public double? QuantidadeSaída { get; set; }
}
