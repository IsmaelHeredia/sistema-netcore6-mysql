using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Models
{
    public class CambiarUsuarioModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo usuario actual es obligatorio")]
        public string Usuario_actual { get; set; }
        [Required(ErrorMessage = "El campo nuevo usuario es obligatorio")]
        public string Nuevo_usuario { get; set; }
        [Required(ErrorMessage = "El campo clave actual es obligatorio")]
        public string Clave_actual { get; set; }
    }
}
