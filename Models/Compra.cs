using System.Data.SqlTypes;

namespace agropindas.Models;

public class Compra
{
    public int Id { get; set; }
    public int IdFornecedor {  get; set; }
    public Fornecedor? Fornecedor { get; set; }
    public DateOnly DataCompra { get; set; }
    public decimal ValorTotal { get; set; }

    public void CalculaValorTotal(IEnumerable<Lote>lotes)
    {
        decimal valorTotal = 0m;

        foreach(var l in lotes)
        {
            valorTotal += l.Produto.ValorProduto * l.QuantidadeLote;
        }

        ValorTotal = valorTotal;
    }
}
