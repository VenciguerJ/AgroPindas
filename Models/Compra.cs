using System.Data.SqlTypes;

namespace agropindas.Models;

public class Compra
{
    public int Id { get; set; }
    public int IdFornecedor {  get; set; }
    public Fornecedor? Fornecedor { get; set; }
    public DateTime DataCompra { get; set; }
    public decimal ValorTotal { get; set; }
    
}
