namespace Db2Seeder.API.Helpers
{
    public class Settings
    {

        public static string GetApiUrl()
        {
            return "http://192.168.210.27:24266";

        }
        public static string GetPortalUrl()
        {
            return "http://my-nis-uat.loteklabs.com/SupportRequest/Detail/";

        }

       
        public static string GetEnviroment()
        {
            return "TT";
            //return "NI";

        }
    }
}
