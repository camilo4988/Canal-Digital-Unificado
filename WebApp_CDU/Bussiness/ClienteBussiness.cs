using WebApp_CDU.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.Helpers;
using WebApp_CDU.Utilities;

namespace WebApp_CDU.Bussiness
{
    public class ClienteBussiness
    {
        public static Cliente AutenticarCliente(long identificacion, string ClaveO4UltDigProd, bool conClave)
        {
            try
            {
                string claveOProducto = conClave ? HashMD5.ToHexString(ClaveO4UltDigProd) : ClaveO4UltDigProd;

                string operacion = conClave ? "AutenticarClienteConClave" : "AutenticarClienteConProducto";

                string url = "Clientes/" + operacion + "/" + identificacion + "/" + claveOProducto;

                var jsonResult = ConsumosServicios.ConsumirServicioGet(url);
                Cliente cliente = JsonConvert.DeserializeObject<Cliente>(jsonResult);

                return cliente;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool ActualizarDatosCliente(Cliente c)
        {
            try
            {
                string url = string.Format("Clientes/ActualizarCliente/{0}/{1}/{2}/{3}/{4}/{5}/{6}/{7}", 
                                            c.Identificacion, 
                                            string.IsNullOrEmpty(c.CorreoElectronico) ? "null" : c.CorreoElectronico, 
                                            string.IsNullOrEmpty(c.Celular) ? "null" : c.Celular, 
                                            string.IsNullOrEmpty(c.TelFijo) ? "null" : c.TelFijo, 
                                            c.Direccion.Replace("#", "NRO"), 
                                            c.Barrio, 
                                            c.Municipio, 
                                            c.Departamento);

                var jsonResult = ConsumosServicios.ConsumirServicioGet(url);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}