namespace Bored.API
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// The main program class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The entry point for the application.
        /// </summary>
        /// <param name="args">The program's arguments.</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Builds the host.
        /// </summary>
        /// <param name="args">The arguements.</param>
        /// <returns>The host builder.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
