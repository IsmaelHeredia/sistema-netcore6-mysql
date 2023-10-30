using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Models
{
    public class ProductoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo nombre es obligatorio")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "El campo descripción es obligatorio")]
        public string? Descripcion { get; set; }
        [Required(ErrorMessage = "El campo precio es obligatorio")]
        public int? Precio { get; set; }
        [Required(ErrorMessage = "El campo proveedor es obligatorio")]
        public int? Id_proveedor { get; set; }
        public string? Fecha_registro { get; set; }
        [ValidateNever]
        public ProveedorModel? Proveedor { get; set; }

        public ProductoModel()
        {
            Proveedor = new ProveedorModel();
        }
    }
}
