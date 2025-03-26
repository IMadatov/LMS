namespace General;

public static class GlobalConstants
{
    public static string[] AllowedHosts
        => new[]
        {
            //Front Local
            "http://localhost:4200",

           //Microservices
           "http://localhost:61273",//auth.api
           "http://localhost:61271",//gw

        };
}