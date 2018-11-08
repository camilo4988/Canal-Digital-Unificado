using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace WebApp_CDU.Models
{
    public class Autenticacion
    {
        [Required]
        [StringLength(maximumLength: 15, MinimumLength = 1)]
        public long Identificacion { set; get; }

        [Required]
        [StringLength(maximumLength: 4, MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string Clave { set; get; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(maximumLength: 4, MinimumLength = 4, ErrorMessage = "Este campo debe ser un número de 4 dígitos.")]
        [Display(Name = "4 últimos dígitos de uno de tus productos")]
        public string Ult4DigProd { set; get; }
    }
}
