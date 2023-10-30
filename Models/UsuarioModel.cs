using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo nombre es obligatorio")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "El campo clave es obligatorio")]
        public string? Clave { get; set; }
        [Required(ErrorMessage = "El campo tipo es obligatorio")]
        public int? Id_tipo { get; set; }
        public string? Fecha_registro { get; set; }
        [ValidateNever]
        public TipoUsuarioModel? TipoUsuario { get; set; }
        public UsuarioModel()
        {
            TipoUsuario = new TipoUsuarioModel();
        }
    }
}
