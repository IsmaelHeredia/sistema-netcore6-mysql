using System.ComponentModel.DataAnnotations;

namespace Sistema.Models
{
    public class IngresoModel
    {
        [Required(ErrorMessage = "El campo usuario es obligatorio")]
        public string Usuario { get; set; }
        [Required(ErrorMessage = "El campo clave es obligatorio")]
        public string Clave { get; set; }
    }
}
