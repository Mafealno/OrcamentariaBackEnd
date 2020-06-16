using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OrcamentariaBackEnd.Database;
using OrcamentariaBackEnd.Repositories;
using OrcamentariaBackEnd.Repositories.Cadastro;

namespace OrcamentariaBackEnd
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddSingleton<IConexao>(sp => new Conexao(connectionString));
            services.AddScoped<IPessoaRepository>(sp => new PessoaRepository(sp.GetService<IConexao>(), sp.GetService<IContatoRepository>(), sp.GetService<IEnderecoRepository>()));
            services.AddScoped<IContatoRepository>(sp => new ContatoRepository(sp.GetService<IConexao>()));
            services.AddScoped<IEnderecoRepository>(sp => new EnderecoRepository(sp.GetService<IConexao>()));
            services.AddScoped<IMaterialRepository>(sp => new MaterialRepository(sp.GetService<IConexao>(), sp.GetService<IPessoaRepository>()));
            services.AddScoped<ICartaCoberturaRepository>(sp => new CartaCoberturaRepository(sp.GetService<IConexao>(), sp.GetService<IMaterialRepository>(), sp.GetService<IItensCartaCoberturaRepository>()));
            services.AddScoped<IItensCartaCoberturaRepository>(sp => new ItensCartaCoberturaRepository(sp.GetService<IConexao>()));
            services.AddScoped<IFuncionarioRepository>(sp => new FuncionarioRepository(sp.GetService<IConexao>(), sp.GetService<IContatoRepository>(), sp.GetService<IEnderecoRepository>()));
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
