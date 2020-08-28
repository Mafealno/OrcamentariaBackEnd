using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using OrcamentariaBackEnd.Database;
using OrcamentariaBackEnd.Repositories;

namespace OrcamentariaBackEnd
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddControllers()
            .AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);

            //INTERFACE
            services.AddSingleton<IConexao>(sp => new Conexao(connectionString));
            services.AddScoped<IMetodosGenericosRepository>(sp => new MetodosGenericosRepository(sp.GetService<IConexao>()));
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
            services.AddScoped<IEquipamentoOrcamentoRepository>(sp => new EquipamentoOrcamentoRepository(sp.GetService<IConexao>()));
            services.AddScoped<IItensOrcamentoRepository>(sp => new ItensOrcamentoRepository(sp.GetService<IConexao>()));
            services.AddScoped<IItensOrcamentoGeralRepository>(sp => new ItensOrcamentoGeralRepository(sp.GetService<IConexao>()));
            services.AddScoped<IItensOrcamentoIntumescenteRepository>(sp => new ItensOrcamentoIntumescenteRepository(sp.GetService<IConexao>()));
            services.AddScoped<IOrcamentoRepository>(sp => new OrcamentoRepository(sp.GetService<IConexao>()));
            services.AddScoped<IOrcamentoIntumescenteRepository>(sp => new OrcamentoIntumescenteRepository(sp.GetService<IConexao>()));


            //SERVICE
            services.AddScoped(sp => new MetodosGenericosService(sp.GetService<IMetodosGenericosRepository>(), sp.GetService<IConfiguration>()));
            services.AddScoped(sp => new PessoaService(sp.GetService<IPessoaRepository>(), sp.GetService<MetodosGenericosService>(), sp.GetService<ContatoService>(), sp.GetService<EnderecoService>()));
            services.AddScoped(sp => new ContatoService(sp.GetService<IContatoRepository>(), sp.GetService<MetodosGenericosService>()));
            services.AddScoped(sp => new EnderecoService(sp.GetService<IEnderecoRepository>(), sp.GetService<MetodosGenericosService>()));
            services.AddScoped(sp => new FuncionarioService(sp.GetService<IFuncionarioRepository>(), sp.GetService<MetodosGenericosService>(), sp.GetService<ContatoService>(), sp.GetService<EnderecoService>(), sp.GetService<PessoaService>()));
            services.AddScoped(sp => new MaterialService(sp.GetService<IMaterialRepository>(), sp.GetService<MetodosGenericosService>(), sp.GetService<PessoaService>(), sp.GetService<ItensCartaCoberturaService>()));
            services.AddScoped(sp => new CartaCoberturaService(sp.GetService<ICartaCoberturaRepository>(), sp.GetService<MetodosGenericosService>(), sp.GetService<ItensCartaCoberturaService>(), sp.GetService<MaterialService>()));
            services.AddScoped(sp => new ItensCartaCoberturaService(sp.GetService<IItensCartaCoberturaRepository>(), sp.GetService<MetodosGenericosService>()));
            services.AddScoped(sp => new PerfilService(sp.GetService<IPerfilRepository>(), sp.GetService<MetodosGenericosService>()));
            services.AddScoped(sp => new EquipamentoService(sp.GetService<IEquipamentoRepository>(), sp.GetService<MetodosGenericosService>(), sp.GetService<PessoaService>()));
            services.AddScoped(sp => new CustoService(sp.GetService<ICustoRepository>(), sp.GetService<MetodosGenericosService>()));

            services.AddScoped(sp => new MaoObraOrcamentoService(sp.GetService<IMaoObraOrcamentoRepository>(), sp.GetService<MetodosGenericosService>(), sp.GetService<FuncionarioService>(), sp.GetService<CustosMaoObraService>()));
            services.AddScoped(sp => new CustosMaoObraService(sp.GetService<ICustosMaoObraRepository>(), sp.GetService<MetodosGenericosService>()));
            services.AddScoped(sp => new CustoOrcamentoService(sp.GetService<ICustoOrcamentoRepository>(), sp.GetService<MetodosGenericosService>(), sp.GetService<CustoService>()));
            services.AddScoped(sp => new EquipamentoOrcamentoService(sp.GetService<IEquipamentoOrcamentoRepository>(), sp.GetService<MetodosGenericosService>(), sp.GetService<EquipamentoService>()));
            services.AddScoped(sp => new ItensOrcamentoService(sp.GetService<IItensOrcamentoRepository>(), sp.GetService<MetodosGenericosService>(), sp.GetService<MaterialService>()));
            services.AddScoped(sp => new ItensOrcamentoGeralService(sp.GetService<IItensOrcamentoGeralRepository>(), sp.GetService<MetodosGenericosService>(), sp.GetService<ItensOrcamentoService>(), sp.GetService<MaterialService>()));
            services.AddScoped(sp => new ItensOrcamentoIntumescenteService(sp.GetService<IItensOrcamentoIntumescenteRepository>(), sp.GetService<MetodosGenericosService>(), sp.GetService<ItensOrcamentoService>(), sp.GetService<MaterialService>(), sp.GetService<PerfilService>(), sp.GetService<CartaCoberturaService>()));
            services.AddScoped(sp => new OrcamentoService(sp.GetService<IOrcamentoRepository>(), sp.GetService<MetodosGenericosService>(), sp.GetService<PessoaService>(), sp.GetService<ItensOrcamentoGeralService>(), sp.GetService<MaoObraOrcamentoService>(), sp.GetService<EquipamentoOrcamentoService>(), sp.GetService<CustoOrcamentoService>(), sp.GetService<TotaisOrcamentoService>()));
            services.AddScoped(sp => new OrcamentoIntumescenteService(sp.GetService<IOrcamentoIntumescenteRepository>(), sp.GetService<MetodosGenericosService>(), sp.GetService<PessoaService>(), sp.GetService<ItensOrcamentoIntumescenteService>(), sp.GetService<MaoObraOrcamentoService>(), sp.GetService<EquipamentoOrcamentoService>(), sp.GetService<CustoOrcamentoService>(), sp.GetService<TotaisOrcamentoService>()));
            services.AddScoped(sp => new TotaisOrcamentoService(sp.GetService<ITotaisOrcamentoRepository>(), sp.GetService<MetodosGenericosService>()));


            services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressConsumesConstraintForFormFileParameters = false;
                    options.SuppressInferBindingSourcesForParameters = true;
                    options.SuppressModelStateInvalidFilter = false;
                    options.SuppressMapClientErrors = false;
                    options.ClientErrorMapping[StatusCodes.Status404NotFound].Link =
                        "https://httpstatuses.com/404";
                });

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:3000")
                                      .WithHeaders(HeaderNames.ContentType, "application/json")
                                      .WithMethods("PUT", "DELETE", "GET", "POST", "OPTIONS"); ;
                                  });
            });
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

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
