namespace Clients
{
    public static class ServicesURL
    {
#if DEBUG
        static string BaseURL = "host.docker.internal";
#else
        static string BaseURL = "localhost";
#endif

        public static Uri TelegramBot =>new Uri($"http://{BaseURL}:5111");            
        public static Uri Auth => new Uri($"http://{BaseURL}:5290");

    }
}
