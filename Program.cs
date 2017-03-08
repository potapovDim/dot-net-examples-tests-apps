using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace ConsoleApplication
{
  public class GrettingService
  {

  }
  public class Startup
  {
    public Startup(IHostingEnvironment env)
    {
      var configurationBuilder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("greetings.json", optional: false, reloadOnChange: true);

      Configuration = configurationBuilder.Build();
    }
    public IConfiguration Configuration { get; set; }
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddRouting();
    }
    public void Configure(IApplicationBuilder app)
    {
      var routeBuilder = new RouteBuilder(app);
      routeBuilder.MapGet("{route}", context =>
      {
        var routeMessage = Configuration.AsEnumerable()
            .FirstOrDefault(r => r.Key == context.GetRouteValue("route")
            .ToString())
            .Value;
        var defaultMessage = Configuration.AsEnumerable()
                .FirstOrDefault(r => r.Key == "default")
                .Value;
        var response = (routeMessage != null) ? routeMessage : defaultMessage;

        return context.Response.WriteAsync(response);
      });
      app.UseRouter(routeBuilder.Build());
    }
  }

  public class Program
  {
    public static void Main(string[] args)
    {
      var host = new WebHostBuilder()
          .UseKestrel()
          .UseStartup<Startup>()
          .Build();

      host.Run();
    }
  }
}