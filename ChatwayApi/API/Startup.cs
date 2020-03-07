using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hub.Bridges;
using Hub.Handlers;
using Hub.Hubs;
using Infrastructure.Repositories;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Services.Services;

namespace API {
    public class Startup {

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {

            services.AddSingleton<Connection>();

            services.AddSingleton<EmpresaService>();
            services.AddSingleton<UnidadeService>();
            services.AddSingleton<UsuarioService>();
            services.AddSingleton<ChatService>();
            services.AddSingleton<MensagemService>();
            services.AddSingleton<ChamadoService>();
            services.AddSingleton<AuthService>();

            services.AddSingleton<EmpresaRepository>();
            services.AddSingleton<UnidadeRepository>();
            services.AddSingleton<UsuarioRepository>();
            services.AddSingleton<ChatRepository>();
            services.AddSingleton<MensagemRepository>();
            services.AddSingleton<ChamadoRepository>();
            services.AddSingleton<AuthRepository>();

            services.AddSingleton<UsuarioHandler>();
            services.AddSingleton<ChatBridge>();

            services.AddControllers();
            services.AddSignalR();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();

                endpoints.MapHub<ChatHub>("/chathub");
            });
        }
    }
}
