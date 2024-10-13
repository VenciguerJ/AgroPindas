namespace agropindas.Models;

//Esta classe implementa consultas no banco de tabelas que possuem apenas estas duas colunas abaixo
public class ProdAssets
{
    public int Id { get; set; }
    public string Nome { get; set; }


    public ProdAssets(int id, string nome)
    {
        Id = id;
        Nome = nome;
    }
    public ProdAssets() { }
}
