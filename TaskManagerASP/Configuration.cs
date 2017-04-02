using Microsoft.Extensions.Configuration;

namespace TaskManagerASP
{
    public static class Configuration
    {
        public static IConfiguration GetConfig()
        {
            var builder = new ConfigurationBuilder()
                //TODO: Make the BasePath automatic
                //Change the basepath according to your machine
                .SetBasePath(@"C:\Users\User\Source\Repos\TaskManagerWebApplication\TaskManagerASP\")
                .AddJsonFile("appsettings.json",
                optional: true,
                reloadOnChange: true);

            return builder.Build();
        }
    }
}
