using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WebApp_CDU.Models;

namespace WebApp_CDU.Utilities
{
    public class CargaDatos
    {
        private static string serverPath;
        private static string path;

        static CargaDatos()
        {
            serverPath = Path.Combine(HttpContext.Current.Server.MapPath(@"~/"), "Resources");
            path = Path.Combine(serverPath, "ConfiguracionEntorno.json");
        }

        public static void GuardarConfiguracionEntorno (ConfiguracionEntorno configuracionEntorno)
        {            
            if(!Directory.Exists(serverPath))
            {
                Directory.CreateDirectory(serverPath);
            }

            string dataJson = JsonConvert.SerializeObject(configuracionEntorno, Formatting.Indented);
            
            File.WriteAllText(path, dataJson);
        }

        public static ConfiguracionEntorno CargarConfiguracionEntorno()
        {
            try
            {
                ConfiguracionEntorno configuracionEntorno = new ConfiguracionEntorno();

                string dataJson = File.ReadAllText(path);
                configuracionEntorno = JsonConvert.DeserializeObject<ConfiguracionEntorno>(dataJson);

                return configuracionEntorno;
            }
            catch
            {
                throw;
            }
        }
    }
}