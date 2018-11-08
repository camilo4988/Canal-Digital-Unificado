using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebApp_CDU.Models
{
    public class ConfiguracionEntorno
    {
        public string NombreCanalDigital { set; get; }
        public TipoAutenticacion MetodoAutenticacion { set; get; }
        public bool AsignacionClave { set; get; }
        public bool ConsultaCupoCompra { set; get; }
        public bool ConsultaCupoAvance { set; get; }
        public bool ConsultaValorPagoMinimo { set; get; }
        public bool ConsultaValorPagoTotal { set; get; }
        public bool BloqueoProducto { set; get; }
        public bool RealizarPago { set; get; }
        public bool GeneracionExtracto { set; get; }
        public bool ConsultaPagos { set; get; }
        public int CantidadDiasMovimientos { set; get; }
        public bool ActualizacionDatosCliente { set; get; }
    }
}