using LStudies.UI.Site.Data;
using LStudies.UI.Site.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace LStudies.UI.Site
{
    public class Startup
    {
        //metodo usado para recuperar configuracoes do ficheiro appsettings.json
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RazorViewEngineOptions>(options =>
            {
                //mudar o nome da pasta das areas da app
                options.AreaViewLocationFormats.Clear();
                options.AreaViewLocationFormats.Add(item: "/Modulos/{2}/Views/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add(item: "/Modulos/{2}/Views/Shared/{0}.cshtml");
                options.AreaViewLocationFormats.Add(item: "/Views/Shared/{0}.cshtml");
            });

            //db
            services.AddDbContext<MyDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MyDbContext")));

            services.AddRazorPages();

            //injecao de dependencia
            services.AddTransient<IPedidoRepository, PedidoRepository>();

            services.AddTransient<IOperacaoTransient, Operacao>(); //usar quando não souber qual DI utilizar
            services.AddScoped<IOperacaoScoped, Operacao>(); //recomendado para o mvc e .net core se não for prejudicial
            services.AddSingleton<IOperacaoSingleton, Operacao>(); //uso raro, possibilita que users acedam info de outros quando estes estão conectados ao server
            services.AddSingleton<IOperacaoSingletonInstance>(new Operacao(Guid.Empty)); //
            services.AddTransient<OperacaoService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllerRoute("areas", "{areas:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapAreaControllerRoute("AreaProdutos", "Produtos", "Produtos/{controller=Cadastro}/{action=Index}/{id?}");
                endpoints.MapAreaControllerRoute("AreaVendas", "Vendas", "Vendas/{controller=Pedido}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
