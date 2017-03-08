using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApplication
{
 public class Startup
{
    public void Configure(IApplicationBuilder app)
    {
        
        var routeBuilder = new RouteBuilder(app);
        
        routeBuilder.MapGet("", context => context.Response.WriteAsync("Hello from root!"));
        routeBuilder.MapGet("hello", context => context.Response.WriteAsync("Hello from /hello"));
        routeBuilder.MapGet("hello/{name}", context => context.Response
                                                              .WriteAsync($"Hello, {context.GetRouteValue("name")}"));

        routeBuilder.MapGet("square/{number:int}", context =>
        {
            int number = Convert.ToInt32(context.GetRouteValue("number"));
            return context.Response.WriteAsync($"The square of {number} is {number * number}");
        });

        routeBuilder.MapPost("post", context => context.Response.WriteAsync("Posting!"));

        app.UseRouter(routeBuilder.Build());

    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddRouting();
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