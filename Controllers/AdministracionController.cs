using Microsoft.AspNetCore.Mvc;
using Sistema.Functions;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;

namespace Sistema.Controllers
{
    public class AdministracionController : Controller
    {
        AppFunctions funciones = new AppFunctions();
        Seguridad seguridad = new Seguridad();

        string nombre_sesion = new Configuracion().nombre_sesion;

        [HttpGet("administracion")]
        public IActionResult Index()
        {
            string sesion = HttpContext.Session.GetString(nombre_sesion);
            if (seguridad.ValidarSesionJWT(sesion))
            {
                if (TempData["mensaje"] != null)
                {
                    ViewBag.mensaje = TempData["mensaje"].ToString();
                }

                string nombre = seguridad.cargarNombreToken(sesion);
                ViewBag.usuario = nombre;
                return View();
            } else
            {
                return RedirectToAction("Ingreso", "Home");
            }
        }

        public IActionResult Salir()
        {
            if (seguridad.ValidarSesionJWT(HttpContext.Session.GetString(nombre_sesion)))
            {
                HttpContext.Session.Remove(nombre_sesion);
                TempData["mensaje"] = funciones.generate_alert("Sesion cerrada", "success");
                return RedirectToAction("Ingreso", "Home");
            } else
            {
                return RedirectToAction("Ingreso", "Home");
            }
        }

    }
}
