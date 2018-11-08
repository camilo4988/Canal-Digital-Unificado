using WebApp_CDU.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp_CDU.Bussiness;
using System.Configuration;

namespace WebApp_CDU.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            if (Session["Cliente"] != null)
            {
                CerrarSesion();
            }

            ViewBag.TipoAutenticacion = Singleton.Instance.ConfiguracionEntorno.MetodoAutenticacion;
            ViewBag.RecaptchaPublicKey = ConfigurationManager.AppSettings["RecaptchaPublicKey"];
            return View();
        }

        // GET: Usuario/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
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

        // GET: Usuario/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Usuario/Edit/5
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

        // GET: Usuario/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Usuario/Delete/5
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
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ValidateRecaptcha]
        public ActionResult Autenticar(FormCollection collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new Respuesta() { Resultado = "Error", Mensaje = "Validación de Captcha fallida." }, JsonRequestBehavior.AllowGet);
                }

                bool autenticarConClave = Singleton.Instance.ConfiguracionEntorno.MetodoAutenticacion == TipoAutenticacion.Clave;

                long identificacion = Convert.ToInt64(collection["Identificacion"]);
                string clave = autenticarConClave ? collection["Clave"] : collection["Ult4DigProd"];

                Cliente cliente = ClienteBussiness.AutenticarCliente(identificacion, clave, autenticarConClave);
                cliente.Productos = ProductoBussiness.ObtenerProductosActivosCliente(cliente.Identificacion);

                Session["Cliente"] = cliente;

                //return PartialView("~/Views/Cliente/InfoCliente.cshtml", cliente);
                return View("~/Views/Cliente/InfoCliente.cshtml", cliente);
            }
            catch
            {
                return Json(new Respuesta() { Resultado = "Error", Mensaje = "Error de autenticación, por favor verifique los datos."}, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult ObtenerInformacionCliente()
        {
            try
            {
                if (Session["Cliente"] != null)
                {

                    Cliente cliente = (Cliente) Session["Cliente"];

                    return PartialView("~/Views/Cliente/InfoCliente.cshtml", cliente);
                }
                else
                {
                    return Json(new Respuesta() { Resultado = "SessionError", Mensaje = "Tu sesión ha finalizado." }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(new Respuesta() { Resultado = "Error", Mensaje = "Error obteniendo los datos del cliente." }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult MostrarActualizacionDatos()
        {
            try
            {
                if (Session["Cliente"] != null)
                {

                    Cliente cliente = (Cliente)Session["Cliente"];

                    return PartialView("~/Views/Cliente/ActualizacionDatosCliente.cshtml", cliente);
                }
                else
                {
                    return Json(new Respuesta() { Resultado = "SessionError", Mensaje = "Tu sesión ha finalizado." }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(new Respuesta() { Resultado = "Error", Mensaje = "Error obteniendo los datos del cliente." }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult ActualizarDatosCliente(FormCollection collection)
        {
            try
            {
                if (Session["Cliente"] != null)
                {

                    Cliente cliente = (Cliente)Session["Cliente"];

                    cliente.CorreoElectronico = collection["CorreoElectronico"];
                    cliente.Celular = collection["Celular"];
                    cliente.TelFijo = collection["TelFijo"];
                    cliente.Direccion = collection["Direccion"];
                    cliente.Barrio = collection["Barrio"];
                    cliente.Municipio = collection["Municipio"];
                    cliente.Departamento = collection["Departamento"];

                    if (string.IsNullOrEmpty(cliente.CorreoElectronico) && string.IsNullOrEmpty(cliente.Celular))
                    {
                        return Json(new Respuesta() { Resultado = "Error", Mensaje = "Debes ingresar un correo electrónico o un número de celular." }, JsonRequestBehavior.AllowGet);
                    }                    

                    bool actualizo = ClienteBussiness.ActualizarDatosCliente(cliente);

                    if (actualizo)
                    {
                        Session["Cliente"] = cliente;
                    }

                    return Json(new Respuesta() { Resultado = "Exito", Mensaje = "Tus datos se actualizaron correctamente." }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new Respuesta() { Resultado = "SessionError", Mensaje = "Tu sesión ha finalizado." }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(new Respuesta() { Resultado = "Error", Mensaje = "Ha ocurrido un error durante la actualización de tus datos." }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public void CerrarSesion()
        {
            try
            {
                Session.Clear();
                Session.Abandon();
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        public bool IsSesionAbierta()
        {
            try
            {
                if(Session["Cliente"] != null)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                throw;
            }
        }
    }
}
