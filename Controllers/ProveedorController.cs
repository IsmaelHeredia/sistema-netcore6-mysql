using Microsoft.AspNetCore.Mvc;
using Sistema.Models;
using Sistema.Data;
using System.Reflection.Metadata.Ecma335;
using Org.BouncyCastle.Asn1.Ocsp;
using Sistema.Functions;

namespace Sistema.Controllers
{
    [Route("administracion/proveedores")]
    public class ProveedorController : Controller
    {
        string nombre_sesion = new Configuracion().nombre_sesion;

        ProveedorDatos proveedorDatos = new ProveedorDatos();
        AppFunctions functions = new AppFunctions();
        Seguridad seguridad = new Seguridad();

        [HttpGet("")]
        public IActionResult Listar()
        {
            if (seguridad.ValidarSesionJWT(HttpContext.Session.GetString(nombre_sesion)))
            {
                if (TempData["mensaje"] != null)
                {
                    ViewBag.mensaje = TempData["mensaje"].ToString();
                }

                List<ProveedorModel> proveedores = proveedorDatos.Listar("");
                ViewBag.proveedores = proveedores;
                return View();
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
            if (seguridad.ValidarSesionJWT(HttpContext.Session.GetString(nombre_sesion)))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Ingreso", "Home");
            }
        }

        [HttpPost]
        [Route("guardar")]
        public IActionResult Guardar(ProveedorModel model)
        {
            if (seguridad.ValidarSesionJWT(HttpContext.Session.GetString(nombre_sesion)))
            {
                if (ModelState.IsValid)
                {
                    TempData["mensaje"] = functions.generate_alert("El proveedor fue registrado", "success");
                    model.Fecha_registro = DateTime.Now.ToString("MM/dd/yyyy");
                    var respuesta = proveedorDatos.Agregar(model);
                    return RedirectToAction("Listar");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Ingreso", "Home");
            }
        }

        [HttpGet]
        [Route("editar")]
        public IActionResult Editar(int idProveedor)
        {
            if (seguridad.ValidarSesionJWT(HttpContext.Session.GetString(nombre_sesion)))
            {
                var proveedor = proveedorDatos.Obtener(idProveedor);
                return View(proveedor);
            }
            else
            {
                return RedirectToAction("Ingreso", "Home");
            }
        }

        [HttpPost]
        [Route("editar")]
        public IActionResult Editar(ProveedorModel oProveedor)
        {
            if (seguridad.ValidarSesionJWT(HttpContext.Session.GetString(nombre_sesion)))
            {
                if (ModelState.IsValid)
                {
                    TempData["mensaje"] = functions.generate_alert("El proveedor fue actualizado", "success");
                    proveedorDatos.Actualizar(oProveedor);
                    return RedirectToAction("Listar");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Ingreso", "Home");
            }
        }

        [HttpGet]
        [Route("borrar")]
        public IActionResult Borrar(int idProveedor)
        {
            if (seguridad.ValidarSesionJWT(HttpContext.Session.GetString(nombre_sesion)))
            {
                var proveedor = proveedorDatos.Obtener(idProveedor);
                return View(proveedor);
            }
            else
            {
                return RedirectToAction("Ingreso", "Home");
            }
        }

        [HttpPost]
        [Route("borrar")]
        public IActionResult Borrar(ProveedorModel oProveedor)
        {
            if (seguridad.ValidarSesionJWT(HttpContext.Session.GetString(nombre_sesion)))
            {
                TempData["mensaje"] = functions.generate_alert("El proveedor fue borrado", "success");
                proveedorDatos.Borrar(oProveedor);
                return RedirectToAction("Listar");
            }
            else
            {
                return RedirectToAction("Ingreso", "Home");
            }
        }

    }
}
