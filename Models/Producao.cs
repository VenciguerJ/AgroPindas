using System.ComponentModel.DataAnnotations.Schema;

namespace agropindas.Models;

public class Producao
{
    public int Id { get; set; }
    public int IdLoteUsado { get; set; }
    public int IdProdutoProduzido { get; set; }
    public int QuantidadeProduzido { get; set; }
    public int IdCalha { get; set; }
    public DateTime? DiaColheita { get; set; }

    [Column("finalizada")]
    public bool Finalizada { get; set; }
}
