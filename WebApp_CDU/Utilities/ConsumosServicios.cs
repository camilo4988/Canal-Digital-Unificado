using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace WebApp_CDU.Utilities
{
    public class ConsumosServicios
    {
        public static string ConsumirServicioGet(string urlRoot)
        {
            string jsonResult = string.Empty;

            try
            {
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + urlRoot;
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

                var webrequest = (HttpWebRequest)System.Net.WebRequest.Create(url);
                webrequest.ContentType = "application/json; charset=utf-8";

                using (var response = webrequest.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        jsonResult = reader.ReadToEnd(); 
                    }
                }                
            }
            catch (WebException wex)
            {
                if (wex.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)wex.Response)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            throw new Exception(reader.ReadToEnd());
                        }
                    }
                }
                throw;
            }
            catch
            {
                throw;
            }

            return jsonResult;
        }
    }
}