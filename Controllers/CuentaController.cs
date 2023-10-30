using Microsoft.AspNetCore.Mvc;
using Sistema.Data;
using Sistema.Functions;
using Sistema.Models;

namespace Sistema.Controllers
{
    [Route("administracion/cuenta")]
    public class CuentaController : Controller
    {
        string nombre_sesion = new Configuracion().nombre_sesion;

        Seguridad seguridad = new Seguridad();
        AccesoDatos accesoDatos = new AccesoDatos();
        UsuarioDatos usuarioDatos = new UsuarioDatos();
        AppFunctions functions = new AppFunctions();

        [HttpGet("cambiarUsuario")]
        public IActionResult CambiarUsuario()
        {
            string token = HttpContext.Session.GetString(nombre_sesion);
            if (seguridad.ValidarSesionJWT(token))
            {
                if (TempData["mensaje"] != null)
                {
                    ViewBag.mensaje = TempData["mensaje"].ToString();
                }

                int id = seguridad.CargarIdToken(token);
                string usuario_actual = seguridad.cargarNombreToken(token);
                var model = new CambiarUsuarioModel();
                model.Id = id;
                model.Usuario_actual = usuario_actual;
                return View(model);
            }
            else
            {
                return RedirectToAction("Ingreso", "Home");
            }
        }

        [HttpPost("cambiarUsuario")]
        public IActionResult CambiarUsuario(CambiarUsuarioModel oCuenta)
        {
            string token = HttpContext.Session.GetString(nombre_sesion);

            if (seguridad.ValidarSesionJWT(token))
            {
                int id = seguridad.CargarIdToken(token);
                string usuario_actual = seguridad.cargarNombreToken(token);
                var model = new CambiarUsuarioModel();
                model.Id = id;
                model.Usuario_actual = usuario_actual;

                if (ModelState.IsValid)
                {
                    int id_usuario = oCuenta.Id;
                    string usuario = oCuenta.Usuario_actual;
                    string clave = oCuenta.Clave_actual;

                    IngresoModel ingresoModel = new IngresoModel();
                    ingresoModel.Usuario = usuario;
                    ingresoModel.Clave = clave;

                    if (accesoDatos.Ingreso(ingresoModel))
                    {
                        usuarioDatos.ActualizarNombre(oCuenta);
                        HttpContext.Session.Remove(nombre_sesion);
                        TempData["mensaje"] = functions.generate_alert("Se cambio el nombre de usuario correctamente", "success");
                        return RedirectToAction("Ingreso", "Home");
                    }
                    else
                    {
                        TempData["mensaje"] = functions.generate_alert("Ingreso inválido", "warning");
                        return RedirectToAction("CambiarUsuario");
                    }
                }
                else
                {
                    return View(model);
                }
            }
            else
            {
                return RedirectToAction("Ingreso", "Home");
            }
        }

        [HttpGet("cambiarClave")]
        public IActionResult CambiarClave()
        {
            string token = HttpContext.Session.GetString(nombre_sesion);
            if (seguridad.ValidarSesionJWT(token))
            {
                if (TempData["mensaje"] != null)
                {
                    ViewBag.mensaje = TempData["mensaje"].ToString();
                }

                int id = seguridad.CargarIdToken(token);
                string usuario_actual = seguridad.cargarNombreToken(token);
                var model = new CambiarClaveModel();
                model.Id = id;
                model.Usuario_actual = usuario_actual;
                return View(model);
            }
            else
            {
                return RedirectToAction("Ingreso", "Home");
            }
        }

        [HttpPost("cambiarClave")]
        public IActionResult CambiarClave(CambiarClaveModel oCuenta)
        {
            string token = HttpContext.Session.GetString(nombre_sesion);

            if (seguridad.ValidarSesionJWT(token))
            {
                int id = seguridad.CargarIdToken(token);
                string usuario_actual = seguridad.cargarNombreToken(token);
                var model = new CambiarClaveModel();
                model.Id = id;
                model.Usuario_actual = usuario_actual;

                if (ModelState.IsValid)
                {
                    int id_usuario = oCuenta.Id;
                    string usuario = oCuenta.Usuario_actual;
                    string clave = oCuenta.Clave_actual;

                    IngresoModel ingresoModel = new IngresoModel();
                    ingresoModel.Usuario = usuario;
                    ingresoModel.Clave = clave;

                    if (accesoDatos.Ingreso(ingresoModel))
                    {
                        oCuenta.Nueva_clave = BCrypt.Net.BCrypt.HashPassword(oCuenta.Nueva_clave);
                        usuarioDatos.ActualizarClave(oCuenta);
                        HttpContext.Session.Remove(nombre_sesion);
                        TempData["mensaje"] = functions.generate_alert("Se cambio la clave del usuario correctamente", "success");
                        return RedirectToAction("Ingreso", "Home");
                    }
                    else
                    {
                        TempData["mensaje"] = functions.generate_alert("Ingreso inválido", "warning");
                        return RedirectToAction("CambiarClave");
                    }
                }
                else
                {
                    return View(model);
                }
            }
            else
            {
                return RedirectToAction("Ingreso", "Home");
            }
        }
    }
}
