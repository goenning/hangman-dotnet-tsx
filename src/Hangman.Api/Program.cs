using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;

namespace Hangman.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowAnyOrigin();
            corsBuilder.AllowCredentials();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", corsBuilder.Build());
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            IFileProvider fileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "../../dist/"));
            app.UseCors("AllowAll");
            app.UseDefaultFiles(new DefaultFilesOptions { FileProvider = fileProvider });    
            app.UseStaticFiles(new StaticFileOptions { FileProvider = fileProvider });   
            app.UseMvc();
        }
    }
}
