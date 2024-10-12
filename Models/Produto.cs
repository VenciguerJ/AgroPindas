using System.ComponentModel.DataAnnotations;

namespace agropindas.Models
{
    public class Produto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [MaxLength(45)]
        public string Nome { get; set; }

        [StringLength(90)]
        public string Descricao {  get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public int TemperaturaPlantio{ get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public int DiasColheita { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public UnidadeCadastro UnidadeCadastro { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public int TipoProduto { get; set; }

    }
}
