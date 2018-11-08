using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp_CDU.Bussiness;
using WebApp_CDU.Models;

namespace WebApp_CDU.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult Index()
        {
            return View();
        }

        // GET: Producto/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Producto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Producto/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Producto/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Producto/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Producto/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Producto/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult ObtenerDetalleProducto(long numeroProducto)
        {
            try
            {
                if (Session["Cliente"] != null)
                {
                    Cliente cliente = (Cliente)Session["Cliente"];
                    Producto producto = cliente.Productos.FirstOrDefault(p => p.Numero == numeroProducto);

                    if (producto == null)
                    {
                        throw new Exception("No se encontró un producto con este número.");
                    }

                    producto.Movimientos = ProductoBussiness.ObtenerMovimientosProducto(numeroProducto, Singleton.Instance.ConfiguracionEntorno.CantidadDiasMovimientos);

                    Session["Cliente"] = cliente;

                    InfoProducto infoProducto = new InfoProducto() { Producto = producto };

                    if (Singleton.Instance.ConfiguracionEntorno.ConsultaCupoCompra)
                    { 
                        infoProducto.CupoCompra = ProductoBussiness.ObtenerCupoCompraProducto(numeroProducto);
                    }

                    if (Singleton.Instance.ConfiguracionEntorno.ConsultaCupoAvance)
                    {
                        infoProducto.CupoAvance = ProductoBussiness.ObtenerCupoAvanceProducto(numeroProducto);
                    }

                    if (Singleton.Instance.ConfiguracionEntorno.ConsultaValorPagoMinimo)
                    {
                        infoProducto.ValorPagoMinimo = ProductoBussiness.ObtenerValorPagoMinimoProducto(numeroProducto);
                    }

                    if (Singleton.Instance.ConfiguracionEntorno.ConsultaValorPagoTotal)
                    {
                        infoProducto.ValorPagoTotal = ProductoBussiness.ObtenerValorPagoTotalProducto(numeroProducto);
                    }
                    return PartialView("~/Views/Producto/InfoProducto.cshtml", infoProducto);
                }
                else
                {
                    return Json(new Respuesta() { Resultado = "SessionError", Mensaje = "La sesión ha finalizado." }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(new Respuesta() { Resultado = "Error", Mensaje = "Ha ocurrido un error consultando los datos del producto." }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult ActualizarEstadoProducto(long numeroProducto, string estado)
        {
            try
            {
                if (Session["Cliente"] != null)
                {
                    Cliente cliente = (Cliente)Session["Cliente"];
                    Producto producto = cliente.Productos.FirstOrDefault(p => p.Numero == numeroProducto);

                    if (producto == null)
                    {
                        throw new Exception("No se encontró un producto con este número.");
                    }

                    bool actualizo = ProductoBussiness.ActualizarEstadoProducto(numeroProducto, estado);

                    if (actualizo)
                    {
                        try
                        {
                            cliente.Productos = ProductoBussiness.ObtenerProductosActivosCliente(cliente.Identificacion);
                        }
                        catch
                        {
                            cliente.Productos = new List<Producto>();
                        }
                    }

                    Session["Cliente"] = cliente;

                    return Json(new Respuesta() { Resultado = "Exito", Mensaje = "Se ha actualizado el estado del producto a " + estado + "." }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new Respuesta() { Resultado = "SessionError", Mensaje = "La sesión ha finalizado." }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(new Respuesta() { Resultado = "Error", Mensaje = "Ha ocurrido un error actualizando el estado del producto." }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
