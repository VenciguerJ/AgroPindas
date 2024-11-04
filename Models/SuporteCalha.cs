using System.ComponentModel.DataAnnotations;

namespace agropindas.Models
{
    public class SuporteCalha
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Campo Obrigatório")]
        public int CapacidadeMudas { get; set; }
        
        [Required(ErrorMessage = "Campo Obrigatório")]
        public bool ocupada { get; set; }
    }
}
