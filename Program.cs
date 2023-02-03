using LibraryWebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
    .ConfigureLogging(logging =>
    {
        logging.AddDebug();
        logging.AddConsole();
    }).Build().Run();