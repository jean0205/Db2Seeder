using System.Configuration;

namespace Db2Seeder.API.Helpers
{
    public class Settings
    {
      //el archivo appconfig no me coge los cambios q hago en el momento, tengo q reiniciar el visual studio, no deseable     cl cambiar del base de datos

        public static string GetApiUrl()
        {  
            return "http://192.168.210.90:24266"; // test

           //return "http://192.168.215.10:24266";
        }
      

        public static string SQLDocuments()
        {
           return "Scanned_Documents_test";
          // return "Scanned_Documents";
        }
        public static string OnlineForms()
        {
           return "OnlineForms_Test";
           //return "OnlineForms";
        }
        public static string Unemployment()
        {
             return "Unemployment_Test";
           //return "Unemployment";
        }
    }
}
