using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp_CDU.Models;
using WebApp_CDU.Utilities;

namespace WebApp_CDU.Bussiness
{
    public class ProductoBussiness
    {
        public static List<Producto> ObtenerProductosActivosCliente(long identificacion)
        {
            try
            {
                string url = "Productos/ObtenerProductosActivosCliente/" + identificacion;

                var jsonResult = ConsumosServicios.ConsumirServicioGet(url);
                List<Producto> Productos = JsonConvert.DeserializeObject<List<Producto>>(jsonResult);

                return Productos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Movimiento> ObtenerMovimientosProducto(long numeroProducto, int dias)
        {
            try
            {
                string url = "Movimientos/ObtenerMovimientosProducto/" + numeroProducto + "/" + dias;

                var jsonResult = ConsumosServicios.ConsumirServicioGet(url);
                List<Movimiento> Movimientos = JsonConvert.DeserializeObject<List<Movimiento>>(jsonResult);

                return Movimientos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static decimal ObtenerCupoCompraProducto(long numeroProducto)
        {
            try
            {
                string url = "Productos/ObtenerCupoCompra/" + numeroProducto;

                var jsonResult = ConsumosServicios.ConsumirServicioGet(url);
                decimal cupoCompra = JsonConvert.DeserializeObject<decimal>(jsonResult);

                return cupoCompra;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static decimal ObtenerCupoAvanceProducto(long numeroProducto)
        {
            try
            {
                string url = "Productos/ObtenerCupoAvance/" + numeroProducto;

                var jsonResult = ConsumosServicios.ConsumirServicioGet(url);
                decimal cupoAvance = JsonConvert.DeserializeObject<decimal>(jsonResult);

                return cupoAvance;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static decimal ObtenerValorPagoMinimoProducto(long numeroProducto)
        {
            try
            {
                string url = "Productos/ObtenerValorPagoMinimo/" + numeroProducto;

                var jsonResult = ConsumosServicios.ConsumirServicioGet(url);
                decimal valorPagoMinimo = JsonConvert.DeserializeObject<decimal>(jsonResult);

                return valorPagoMinimo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static decimal ObtenerValorPagoTotalProducto(long numeroProducto)
        {
            try
            {
                string url = "Productos/ObtenerValorPagoTotal/" + numeroProducto;

                var jsonResult = ConsumosServicios.ConsumirServicioGet(url);
                decimal valorPagoTotal = JsonConvert.DeserializeObject<decimal>(jsonResult);

                return valorPagoTotal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool ActualizarEstadoProducto(long numeroProducto, string estado)
        {
            try
            {
                if (!Enum.GetNames(typeof(EstadosProducto)).Contains(estado))
                {
                    throw new Exception("El estado no es valido.");
                }

                string url = "Productos/ActualizarEstadoProducto/" + numeroProducto + "/" + estado;

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