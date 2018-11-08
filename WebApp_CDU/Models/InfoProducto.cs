using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp_CDU.Models
{
    public class InfoProducto
    {
        public Producto Producto {set; get;}

        [Display(Name = "Cupo para compra")]
        public decimal CupoCompra { set; get; }

        [Display(Name = "Cupo para avance")]
        public decimal CupoAvance { set; get; }

        [Display(Name = "Valor de pago mínimo")]
        public decimal ValorPagoMinimo { set; get; }

        [Display(Name = "Valor de pago total")]
        public decimal ValorPagoTotal { set; get; }
    }
}