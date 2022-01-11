using System.Configuration;

namespace Db2Seeder.API.Helpers
{
    public class Settings
    {
      //el archivo appconfig no me coge los cambios q hago en el momento, tengo q reiniciar el visual studio, no deseable     cl cambiar del base de datos

        public static string GetApiUrl()
        {
            //var _configuration = ConfigurationManager.AppSettings;
            //return _configuration["APIURL"];
            return "http://192.168.215.10:24266";

        }
        public static string GetPortalUrl()
        {
            //return ConfigurationManager.AppSettings["PortalURL"];
            return "https://my.nisgrenada.org/SupportRequest/Detail/";
        }

       
        public static string GetEnviroment()
        {
            //return ConfigurationManager.AppSettings["Enviroment"];
            //return "NI";
            return "TT";
        }

        public static string SQLDocuments()
        {
            //return "Scanned_Documents_test";
            return "Scanned_Documents";
        }
        public static string OnlineForms()
        {
            //return "OnlineForms_Test";
            return "OnlineForms";
        }
    }
}
