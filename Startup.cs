using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrcamentariaBackEnd.Database;


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

            //INTERFACE
            services.AddScoped<IMetodosGenericosRepository>(sp => new MetodosGenericosRepository(sp.GetService<IConexao>()));
            services.AddSingleton<IConexao>(sp => new Conexao(connectionString));
            services.AddScoped<IPessoaRepository>(sp => new PessoaRepository(sp.GetService<IConexao>()));
            services.AddScoped<IContatoRepository>(sp => new ContatoRepository(sp.GetService<IConexao>()));
            services.AddScoped<IEnderecoRepository>(sp => new EnderecoRepository(sp.GetService<IConexao>()));
            services.AddScoped<IFuncionarioRepository>(sp => new FuncionarioRepository(sp.GetService<IConexao>()));
            services.AddScoped<IMaterialRepository>(sp => new MaterialRepository(sp.GetService<IConexao>()));
            services.AddScoped<ICartaCoberturaRepository>(sp => new CartaCoberturaRepository(sp.GetService<IConexao>()));
            services.AddScoped<IItensCartaCoberturaRepository>(sp => new ItensCartaCoberturaRepository(sp.GetService<IConexao>()));
            services.AddScoped<IPerfilRepository>(sp => new PerfilRepository(sp.GetService<IConexao>()));
            services.AddScoped<IEquipamentoRepository>(sp => new EquipamentoRepository(sp.GetService<IConexao>()));
            services.AddScoped<ICustoRepository>(sp => new CustoRepository(sp.GetService<IConexao>()));

            services.AddScoped<IMaoObraOrcamentoRepository>(sp => new MaoObraOrcamentoRepository(sp.GetService<IConexao>()));
            services.AddScoped<ICustosMaoObraRepository>(sp => new CustosMaoObraRepository(sp.GetService<IConexao>()));
            services.AddScoped<ICustoOrcamentoRepository>(sp => new CustoOrcamentoRepository(sp.GetService<IConexao>()));


            //SERVICE
            services.AddScoped(sp => new MetodosGenericosService(sp.GetService<IMetodosGenericosRepository>()));
            services.AddScoped(sp => new PessoaService(sp.GetService<IPessoaRepository>(), sp.GetService<MetodosGenericosService>(), sp.GetService<ContatoService>(), sp.GetService<EnderecoService>()));
            services.AddScoped(sp => new ContatoService(sp.GetService<IContatoRepository>(), sp.GetService<MetodosGenericosService>()));
            services.AddScoped(sp => new EnderecoService(sp.GetService<IEnderecoRepository>(), sp.GetService<MetodosGenericosService>()));
            services.AddScoped(sp => new FuncionarioService(sp.GetService<IFuncionarioRepository>(), sp.GetService<MetodosGenericosService>(), sp.GetService<ContatoService>(), sp.GetService<EnderecoService>(), sp.GetService<PessoaService>()));
            services.AddScoped(sp => new MaterialService(sp.GetService<IMaterialRepository>(), sp.GetService<MetodosGenericosService>(), sp.GetService<PessoaService>(), sp.GetService<CartaCoberturaService>()));
            services.AddScoped(sp => new CartaCoberturaService(sp.GetService<ICartaCoberturaRepository>(), sp.GetService<MetodosGenericosService>(), sp.GetService<ItensCartaCoberturaService>(), sp.GetService<MaterialService>()));
            services.AddScoped(sp => new ItensCartaCoberturaService(sp.GetService<IItensCartaCoberturaRepository>(), sp.GetService<MetodosGenericosService>()));
            services.AddScoped(sp => new PerfilService(sp.GetService<IPerfilRepository>(),sp.GetService<MetodosGenericosService>()));
            services.AddScoped(sp => new EquipamentoService(sp.GetService<IEquipamentoRepository>(), sp.GetService<MetodosGenericosService>(), sp.GetService<PessoaService>()));
            services.AddScoped(sp => new CustoService(sp.GetService<ICustoRepository>(), sp.GetService<MetodosGenericosService>()));

            services.AddScoped(sp => new MaoObraOrcamentoService(sp.GetService<IMaoObraOrcamentoRepository>(), sp.GetService<MetodosGenericosService>(), sp.GetService<FuncionarioService>(), sp.GetService<CustosMaoObraService>()));
            services.AddScoped(sp => new CustosMaoObraService(sp.GetService<ICustosMaoObraRepository>(), sp.GetService<MetodosGenericosService>()));
            services.AddScoped(sp => new CustoOrcamentoService(sp.GetService<ICustoOrcamentoRepository>(), sp.GetService<MetodosGenericosService>(), sp.GetService<CustoService>()));

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
