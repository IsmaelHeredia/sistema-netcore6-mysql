using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Models
{
    public class ActualizarUsuarioModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo tipo es obligatorio")]
        public int? Id_tipo { get; set; }
    }
}
