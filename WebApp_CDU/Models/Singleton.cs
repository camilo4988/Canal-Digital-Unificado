using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp_CDU.Utilities;

namespace WebApp_CDU.Models
{
    public class Singleton
    {
        private static Singleton instance = null;
        public ConfiguracionEntorno ConfiguracionEntorno { set; get; }

        protected Singleton() { }

        public static Singleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Singleton();                    
                }

                if (instance.ConfiguracionEntorno == null)
                {
                    instance.ConfiguracionEntorno = CargaDatos.CargarConfiguracionEntorno();
                }
                

                return instance;
            }
        }
    }
}