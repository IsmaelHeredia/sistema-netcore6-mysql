using Microsoft.AspNetCore.Mvc;
using Sistema.Models;
using Sistema.Data;
using Sistema.Functions;

namespace Sistema.Controllers
{
    [Route("administracion/productos")]
    public class ProductoController : Controller
    {
        string nombre_sesion = new Configuracion().nombre_sesion;

        ProductoDatos productoDatos = new ProductoDatos();
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

                List<ProductoModel> productos = productoDatos.Listar("");
                ViewBag.productos = productos;
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
                List<ProveedorModel> proveedores = proveedorDatos.Listar("");
                ViewBag.proveedores = proveedores;

                return View();
            }
            else
            {
                return RedirectToAction("Ingreso", "Home");
            }
        }

        [HttpPost]
        [Route("guardar")]
        public IActionResult Guardar(ProductoModel model)
        {
            if (seguridad.ValidarSesionJWT(HttpContext.Session.GetString(nombre_sesion)))
            {
                if (ModelState.IsValid)
                {
                    TempData["mensaje"] = functions.generate_alert("El producto fue registrado", "success");
                    model.Fecha_registro = DateTime.Now.ToString("MM/dd/yyyy");
                    var respuesta = productoDatos.Agregar(model);
                    return RedirectToAction("Listar");
                }
                else
                {
                    List<ProveedorModel> proveedores = proveedorDatos.Listar("");
                    ViewBag.proveedores = proveedores;

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
        public IActionResult Editar(int idProducto)
        {
            if (seguridad.ValidarSesionJWT(HttpContext.Session.GetString(nombre_sesion)))
            {
                var producto = productoDatos.Obtener(idProducto);

                List<ProveedorModel> proveedores = proveedorDatos.Listar("");
                ViewBag.proveedores = proveedores;

                return View(producto);
            }
            else
            {
                return RedirectToAction("Ingreso", "Home");
            }
        }

        [HttpPost]
        [Route("editar")]
        public IActionResult Editar(ProductoModel oProducto)
        {
            if (seguridad.ValidarSesionJWT(HttpContext.Session.GetString(nombre_sesion)))
            {
                if (ModelState.IsValid)
                {
                    TempData["mensaje"] = functions.generate_alert("El producto fue actualizado", "success");
                    productoDatos.Actualizar(oProducto);
                    return RedirectToAction("Listar");
                }
                else
                {
                    List<ProveedorModel> proveedores = proveedorDatos.Listar("");
                    ViewBag.proveedores = proveedores;

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
        public IActionResult Borrar(int idProducto)
        {
            if (seguridad.ValidarSesionJWT(HttpContext.Session.GetString(nombre_sesion)))
            {
                var producto = productoDatos.Obtener(idProducto);
                return View(producto);
            }
            else
            {
                return RedirectToAction("Ingreso", "Home");
            }
        }

        [HttpPost]
        [Route("borrar")]
        public IActionResult Borrar(ProductoModel oProducto)
        {
            if (seguridad.ValidarSesionJWT(HttpContext.Session.GetString(nombre_sesion)))
            {
                TempData["mensaje"] = functions.generate_alert("El producto fue borrado", "success");
                productoDatos.Borrar(oProducto);
                return RedirectToAction("Listar");
            }
            else
            {
                return RedirectToAction("Ingreso", "Home");
            }
        }
    }
}
