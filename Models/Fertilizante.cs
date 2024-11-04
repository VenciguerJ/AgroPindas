using NuGet.Versioning;

namespace agropindas.Models;

public class Fertilizante : Produto
{
    public int IdSuporte { get; set; }
    public float QtdUtilizada { get; set; }
}
