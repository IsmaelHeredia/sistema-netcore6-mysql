using Microsoft.AspNetCore.Mvc;
using Sistema.Data;
using Sistema.Functions;
using Sistema.Models;

namespace Sistema.Controllers
{
    [Route("administracion/usuarios")]
    public class UsuarioController : Controller
    {
        string nombre_sesion = new Configuracion().nombre_sesion;

        UsuarioDatos usuarioDatos = new UsuarioDatos();
        TipoUsuarioDatos tipousuarioDatos = new TipoUsuarioDatos();
        AppFunctions functions = new AppFunctions();
        Seguridad seguridad = new Seguridad();

        [HttpGet("")]
        public IActionResult Listar()
        {
            string sesion = HttpContext.Session.GetString(nombre_sesion);
            if (seguridad.ValidarSesionJWT(sesion))
            {
                if (seguridad.cargarTipoToken(sesion) == 1)
                {
                    if (TempData["mensaje"] != null)
                    {
                        ViewBag.mensaje = TempData["mensaje"].ToString();
                    }

                    List<UsuarioModel> usuarios = usuarioDatos.Listar("");
                    ViewBag.usuarios = usuarios;
                    return View();
                }
                else
                {
                    TempData["mensaje"] = functions.generate_alert("Acceso denegado", "danger");
                    return RedirectToAction("Index", "Administracion");
                }
            }
            else
            {
                return RedirectToAction("Ingreso", "Home");
            }
        }

        [HttpGet]
        [Route("guardar")]
        public IActionResult Guardar()
        {
            string sesion = HttpContext.Session.GetString(nombre_sesion);
            if (seguridad.ValidarSesionJWT(sesion))
            {
                if (seguridad.cargarTipoToken(sesion) == 1)
                {
                    List<TipoUsuarioModel> tipos_usuarios = tipousuarioDatos.Listar("");
                    ViewBag.tipos_usuarios = tipos_usuarios;

                    return View();
                }
                else
                {
                    TempData["mensaje"] = functions.generate_alert("Acceso denegado", "danger");
                    return RedirectToAction("Index", "Administracion");
                }
            }
            else
            {
                return RedirectToAction("Ingreso", "Home");
            }
        }

        [HttpPost]
        [Route("guardar")]
        public IActionResult Guardar(UsuarioModel model)
        {
            string sesion = HttpContext.Session.GetString(nombre_sesion);
            if (seguridad.ValidarSesionJWT(sesion))
            {
                if (seguridad.cargarTipoToken(sesion) == 1)
                {
                    if (ModelState.IsValid)
                    {
                        TempData["mensaje"] = functions.generate_alert("El usuario fue registrado", "success");
                        model.Clave = BCrypt.Net.BCrypt.HashPassword(model.Clave);
                        model.Fecha_registro = DateTime.Now.ToString("MM/dd/yyyy");
                        var respuesta = usuarioDatos.Agregar(model);
                        return RedirectToAction("Listar");
                    }
                    else
                    {
                        List<TipoUsuarioModel> tipos_usuarios = tipousuarioDatos.Listar("");
                        ViewBag.tipos_usuarios = tipos_usuarios;

                        return View();
                    }
                }
                else
                {
                    TempData["mensaje"] = functions.generate_alert("Acceso denegado", "danger");
                    return RedirectToAction("Index", "Administracion");
                }
            }
            else
            {
                return RedirectToAction("Ingreso", "Home");
            }
        }

        [HttpGet]
        [Route("editar")]
        public IActionResult Editar(int idUsuario)
        {
            string sesion = HttpContext.Session.GetString(nombre_sesion);
            if (seguridad.ValidarSesionJWT(sesion))
            {
                if (seguridad.cargarTipoToken(sesion) == 1)
                {
                    var usuario = usuarioDatos.ObtenerPorId(idUsuario);

                    List<TipoUsuarioModel> tipos_usuarios = tipousuarioDatos.Listar("");
                    ViewBag.tipos_usuarios = tipos_usuarios;

                    return View(usuario);
                }
                else
                {
                    TempData["mensaje"] = functions.generate_alert("Acceso denegado", "danger");
                    return RedirectToAction("Index", "Administracion");
                }
            }
            else
            {
                return RedirectToAction("Ingreso", "Home");
            }
        }

        [HttpPost]
        [Route("editar")]
        public IActionResult Editar(ActualizarUsuarioModel oUsuario)
        {
            string sesion = HttpContext.Session.GetString(nombre_sesion);
            if (seguridad.ValidarSesionJWT(sesion))
            {
                if (seguridad.cargarTipoToken(sesion) == 1)
                {
                    if (ModelState.IsValid)
                    {
                        TempData["mensaje"] = functions.generate_alert("El usuario fue actualizado", "success");
                        usuarioDatos.ActualizarEstado(oUsuario);
                        return RedirectToAction("Listar");
                    }
                    else
                    {
                        List<TipoUsuarioModel> tipos_usuarios = tipousuarioDatos.Listar("");
                        ViewBag.tipos_usuarios = tipos_usuarios;

                        return View();
                    }
                }
                else
                {
                    TempData["mensaje"] = functions.generate_alert("Acceso denegado", "danger");
                    return RedirectToAction("Index", "Administracion");
                }
            }
            else
            {
                return RedirectToAction("Ingreso", "Home");
            }
        }

        [HttpGet]
        [Route("borrar")]
        public IActionResult Borrar(int idUsuario)
        {
            string sesion = HttpContext.Session.GetString(nombre_sesion);
            if (seguridad.ValidarSesionJWT(sesion))
            {
                if (seguridad.cargarTipoToken(sesion) == 1)
                {
                    var usuario = usuarioDatos.ObtenerPorId(idUsuario);
                    return View(usuario);
                }
                else
                {
                    TempData["mensaje"] = functions.generate_alert("Acceso denegado", "danger");
                    return RedirectToAction("Index", "Administracion");
                }
            }
            else
            {
                return RedirectToAction("Ingreso", "Home");
            }
        }

        [HttpPost]
        [Route("borrar")]
        public IActionResult Borrar(UsuarioModel oUsuario)
        {
            string sesion = HttpContext.Session.GetString(nombre_sesion);
            if (seguridad.ValidarSesionJWT(sesion))
            {
                if (seguridad.cargarTipoToken(sesion) == 1)
                {
                    TempData["mensaje"] = functions.generate_alert("El usuario fue borrado", "success");
                    usuarioDatos.Borrar(oUsuario);
                    return RedirectToAction("Listar");
                }
                else
                {
                    TempData["mensaje"] = functions.generate_alert("Acceso denegado", "danger");
                    return RedirectToAction("Index", "Administracion");
                }
            }
            else
            {
                return RedirectToAction("Ingreso", "Home");
            }
        }
    }
}
