using System.ComponentModel.DataAnnotations;

namespace Sistema.Models
{
    public class ProveedorModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo nombre es obligatorio")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "El campo dirección es obligatorio")]
        public string? Direccion { get; set; }
        [Required(ErrorMessage = "El campo teléfono es obligatorio")]
        public int? Telefono { get; set; }
        public string? Fecha_registro { get; set; }

        public ProveedorModel()
        {
        }
    }
}
