using System.ComponentModel.DataAnnotations.Schema;

namespace agropindas.Models;

public class LoteMuda
{
    [Column("id")]
    public int Id { get; set; }

    [Column("id_estoque_produto")]
    public int IdEstoqueProduto { get; set; }

    public Produto? ProdutoMuda { get; set; }

    [Column("id_producao")]
    public int IdProducao { get; set; }

    [Column("quantidade_inicial")]
    public int QuantidadeInicial { get; set; }

    [Column("quantidade_vendido")]
    public int QuantidadeVendido { get; set; }
}
