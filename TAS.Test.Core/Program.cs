using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace TAS.Test.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            // Url de ejemplo
            var mUrl = "https://my-json-server.typicode.com/utn-frcu-isi-tdp/tas-db/clients?id=12345678&pass=1234";

            // Se crea el request http
            HttpWebRequest mRequest = (HttpWebRequest)WebRequest.Create(mUrl);

            try
            {
                // Se ejecuta la consulta
                WebResponse mResponse = mRequest.GetResponse();

                // Se obtiene los datos de respuesta
                using (Stream responseStream = mResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);

                    // Se parsea la respuesta y se serializa a JSON a un objeto dynamic
                    dynamic mResponseJSON = JsonConvert.DeserializeObject(reader.ReadToEnd());

                    //System.Console.WriteLine("Código de respuesta: {0}", mResponseJSON.response_code);
                    if (mResponseJSON.Count >= 1)
                    {
                        System.Console.WriteLine("Item completo -> {0}", mResponseJSON[0].response);
                        System.Console.WriteLine("Nombre -> {0}", mResponseJSON[0].response.client.name);
                        System.Console.WriteLine("Segmento -> {0}", mResponseJSON[0].response.client.segment);
                    }
                    else
                    {
                        System.Console.WriteLine("No se devolvieron datos");
                    }
                }
            }
            catch (WebException ex)
            {
                WebResponse mErrorResponse = ex.Response;
                using (Stream mResponseStream = mErrorResponse.GetResponseStream())
                {
                    StreamReader mReader = new StreamReader(mResponseStream, Encoding.GetEncoding("utf-8"));
                    String mErrorText = mReader.ReadToEnd();

                    System.Console.WriteLine("Error: {0}", mErrorText);
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error: {0}", ex.Message);
            }

            System.Console.ReadLine();
        }
    }
    
}
