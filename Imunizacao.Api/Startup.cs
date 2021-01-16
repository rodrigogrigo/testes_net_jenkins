using RgCidadao.Api.Filters;
using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using RgCidadao.Domain.Repositories.Cadastro;
using RgCidadao.Domain.Infra.Repositories.Cadastro;
using RgCidadao.Domain.Commands.Cadastro;
using RgCidadao.Domain.Queries.Cadastro;

using RgCidadao.Domain.Repositories.Imunizacao;
using RgCidadao.Domain.Infra.Repositories.Imunizacao;
using RgCidadao.Domain.Commands.Imunizacao;
using RgCidadao.Domain.Queries.Imunizacao;

//using RgCidadao.Domain.Repositories.E_SUS;
//using RgCidadao.Domain.Infra.Repositories.E_SUS;
//using RgCidadao.Domain.Commands.E_SUS;
//using RgCidadao.Domain.Queries.E_SUS;

using Rotativa.AspNetCore;

using RgCidadao.Domain.Repositories.Endemias;
using RgCidadao.Domain.Infra.Repositories.Endemias;
using RgCidadao.Domain.Commands.Endemias;
using RgCidadao.Domain.Queries.Endemias;

using RgCidadao.Domain.Repositories.Seguranca;
using RgCidadao.Domain.Infra.Repositories.Seguranca;
using RgCidadao.Domain.Commands.Seguranca;
using RgCidadao.Domain.Queries.Seguranca;

using RgCidadao.Domain.Repositories.AtencaoBasica;
using RgCidadao.Domain.Infra.Repositories.AtencaoBasica;
using RgCidadao.Domain.Commands.AtencaoBasica;
using RgCidadao.Domain.Queries.AtencaoBasica;

using RgCidadao.Domain.Repositories.Indicadores;
using RgCidadao.Domain.Infra.Repositories.Indicadores;
using RgCidadao.Domain.Commands.Indicadores;
using RgCidadao.Domain.Queries.Indicadores;

using RgCidadao.Domain.Repositories.Prontuario;
using RgCidadao.Domain.Infra.Repositories.Prontuario;
using RgCidadao.Domain.Commands.Prontuario;
using RgCidadao.Domain.Queries.Prontuario;

namespace RgCidadao.Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            #region REGISTRO DE INTERFACES E CLASSES


            #region Atenção Básica
            services.AddTransient<IAgendaRepository, AgendaRepository>();
            services.AddTransient<IAgendaCommand, AgendaCommandText>();

            services.AddTransient<IAtendOdontoRepository, AtendOdontoRepository>();
            services.AddTransient<IAtendOdontoCommand, AtendOdontoCommandText>();

            services.AddTransient<IAtividadeColetivaRepository, AtividadeColetivaRepository>();
            services.AddTransient<IAtividadeColetivaCommand, AtividadeColetivaCommandText>();

            services.AddTransient<IConsumoAlimentarRepository, ConsumoAlimentarRepository>();
            services.AddTransient<IConsumoAlimentarCommand, ConsumoAlimentarCommandText>();

            services.AddTransient<Domain.Repositories.AtencaoBasica.IEstabelecimentoRepository, Domain.Infra.Repositories.AtencaoBasica.EstabelecimentoRepository>();
            services.AddTransient<Domain.Commands.AtencaoBasica.IEstabelecimentoCommand, Domain.Queries.AtencaoBasica.EstabelecimentoCommandText>();

            services.AddTransient<IEstabelecimentoSaudeRepository, EstabelecimentoSaudeRepository>();
            services.AddTransient<IEstabelecimentoSaudeCommand, EstabelecimentoSaudeCommandText>();

            services.AddTransient<IExameFisicoRepository, ExameFisicoRepository>();
            services.AddTransient<IExameFisicoCommand, ExameFisicoCommandText>();

            services.AddTransient<IFamiliaRepository, FamiliaRepository>();
            services.AddTransient<IFamiliaCommand, FamiliaCommandText>();

            services.AddTransient<IFichaComplementarRepository, FichaComplementarRepository>();
            services.AddTransient<IFichaComplementarCommand, FichaComplementarCommandText>();

            services.AddTransient<IGestaoFamiliaRepository, GestaoFamiliaRepository>();
            services.AddTransient<IGestaoFamiliaCommand, GestaoFamiliaCommandText>();

            services.AddTransient<IMicroareaRepository, MicroareaRepository>();
            services.AddTransient<IMicroareaCommand, MicroareaCommandText>();

            services.AddTransient<IProcedimentoAvulsoRepository, ProcedimentoAvulsoRepository>();
            services.AddTransient<IProcedimentoAvulsoCommand, ProcedimentoAvulsoCommandText>();

            services.AddTransient<IProcedimentoRepository, ProcedimentoRepository>();
            services.AddTransient<IProcedimentoCommand, ProcedimentoCommandText>();

            services.AddTransient<IVisitaDomiciliarRepository, VisitaDomiciliarRepository>();
            services.AddTransient<IVisitaDomiciliarCommand, VisitaDomiciliarCommandText>();

            services.AddTransient<IFamiliaRepository, FamiliaRepository>();
            services.AddTransient<IFamiliaCommand, FamiliaCommandText>();
            #endregion

            #region Cadastro
            services.AddTransient<IACSRepository, ACSRepository>();
            services.AddTransient<IACSCommand, ACSCommandText>();

            services.AddTransient<IBairroRepository, BairroRepository>();
            services.AddTransient<IBairroCommand, BairroCommandText>();

            services.AddTransient<ICidadaoRepository, CidadaoRepository>();
            services.AddTransient<ICidadaoCommand, CidadaoCommandText>();

            services.AddTransient<ICidadeRepository, CidadeRepository>();
            services.AddTransient<ICidadeCommand, CidadeCommandText>();

            services.AddTransient<IEquipeRepository, EquipeRepository>();
            services.AddTransient<IEquipeCommand, EquipeCommandText>();

            services.AddTransient<IEscolaRepository, EscolaRepository>();
            services.AddTransient<IEscolaCommand, EscolaCommandText>();

            services.AddTransient<IEstadoRepository, EstadoRepository>();
            services.AddTransient<IEstadoCommand, EstadoCommandText>();

            services.AddTransient<IFeriadoRepository, FeriadoRepository>();
            services.AddTransient<IFeriadoCommand, FeriadoCommandText>();

            services.AddTransient<IFornecedorRepository, FornecedorRepository>();
            services.AddTransient<IFornecedorCommand, FornecedorCommandText>();

            services.AddTransient<IFotoIndividuoRepository, FotoIndividuoRepository>();
            services.AddTransient<IFotoIndividuoCommand, FotoIndividuoCommandText>();

            services.AddTransient<IGestacaoRepository, GestacaoRepository>();
            services.AddTransient<IGestacaoCommand, GestacaoCommandText>();

            services.AddTransient<IIndividuoRepository, IndividuoRepository>();
            services.AddTransient<IIndividuoCommand, IndividuoCommandText>();

            services.AddTransient<ILogradouroRepository, LogradouroRepository>();
            services.AddTransient<ILogradouroCommand, LogradouroCommandText>();

            services.AddTransient<IPaisRepository, PaisRepository>();
            services.AddTransient<Domain.Commands.Cadastro.IPaisCommand, PaisCommandText>();

            services.AddTransient<IProfissaoRepository, ProfissaoRepository>();
            services.AddTransient<Domain.Commands.Cadastro.IProfissaoCommand, ProfissaoCommandText>();

            services.AddTransient<IProfissionalRepository, ProfissionalRepository>();
            services.AddTransient<Domain.Commands.Cadastro.IProfissionalCommand, ProfissionalCommandText>();

            services.AddTransient<IUnidadeRepository, UnidadeRepository>();
            services.AddTransient<IUnidadeCommand, UnidadeCommandText>();

            services.AddTransient<ISegUserRepository, SegUserRepository>();
            services.AddTransient<ISegUsuarioCommand, SegUsuarioCommandText>();

            services.AddTransient<IVersaoRepository, VersaoRepository>();
            services.AddTransient<IVersaoCommand, VersaoCommandText>();
            #endregion

            #region Endemias
            services.AddTransient<ICicloRepository, CicloRepository>();
            services.AddTransient<ICicloCommand, CicloCommandText>();

            services.AddTransient<IEspecimeRepository, EspecimeRepository>();
            services.AddTransient<IEspecimeCommand, EspecimeCommandText>();

            services.AddTransient<Domain.Repositories.Endemias.IEstabelecimentoRepository, Domain.Infra.Repositories.Endemias.EstabelecimentoRepository>();
            services.AddTransient<Domain.Commands.Endemias.IEstabelecimentoCommand, Domain.Queries.Endemias.EstabelecimentoCommandText>();

            services.AddTransient<IReportEndemiasRepository, ReportEndemiasRepository>();
            services.AddTransient<IReportEndemiasCommand, ReportEndemiasCommandText>();

            services.AddTransient<IResultadoAmostraRepository, ResultadoAmostraRepository>();
            services.AddTransient<IResultadoAmostraCommand, ResultadoAmostraCommandText>();

            services.AddTransient<IVisitaRepository, VisitaRepository>();
            services.AddTransient<IVisitaCommand, VisitaCommandText>();

            #endregion

            #region Imunização
            services.AddTransient<IAprazamentoRepository, AprazamentoRepository>();
            services.AddTransient<IAprazamentoCommand, AprazamentoCommandText>();

            services.AddTransient<ICalendarioBasicoRepository, CalendarioBasicoRepository>();
            services.AddTransient<ICalendarioBasicoCommand, CalendarioBasicoCommandText>();

            services.AddTransient<ICartaoVacinaRepository, CartaoVacinaRepository>();
            services.AddTransient<ICartaoVacinaCommand, CartaoVacinaCommandText>();

            services.AddTransient<IClasseRepository, ClasseRepository>();
            services.AddTransient<IClasseCommand, ClasseCommandText>();

            services.AddTransient<IDashboardRepository, DashboardRepository>();
            services.AddTransient<IDashboardCommand, DashboardCommandText>();

            services.AddTransient<IDoseRepository, DoseRepository>();
            services.AddTransient<IDoseCommand, DoseCommandText>();

            services.AddTransient<IEntradaProdutoRepository, EntradaProdutoRepository>();
            services.AddTransient<IEntradaProdutoCommand, EntradaProdutoCommandText>();

            services.AddTransient<IEntradaProdutoItemRepository, EntradaProdutoItemRepository>();
            services.AddTransient<IEntradaProdutoItemCommand, EntradaProdutoItemCommandText>();

            services.AddTransient<IEnvioRepository, EnvioRepository>();
            services.AddTransient<IEnvioCommand, EnvioCommandText>();

            services.AddTransient<IEstoqueRepository, EstoqueRepository>();
            services.AddTransient<IEstoqueCommand, EstoqueCommandText>();

            services.AddTransient<IEstrategiaRepository, EstrategiaRepository>();
            services.AddTransient<IEstrategiaCommand, EstrategiaCommandText>();

            services.AddTransient<IFabricanteRepository, FabricanteRepository>();
            services.AddTransient<IFabricanteCommand, FabricanteCommandText>();

            services.AddTransient<IFaixaEtariaRepository, FaixaEtariaRepository>();
            services.AddTransient<IFaixaEtariaCommand, FaixaEtariaCommandText>();

            services.AddTransient<IGrupoAtendimentoRepository, GrupoAtendimentoRepository>();
            services.AddTransient<IGrupoAtendimentoCommand, GrupoAtendimentoCommandText>();

            services.AddTransient<IImunobiologicoRepository, ImunobiologicoRepository>();
            services.AddTransient<IImunobiologicoCommand, ImunobiologicoCommandText>();

            services.AddTransient<ILoteRepository, LoteRepository>();
            services.AddTransient<ILoteCommand, LoteCommandText>();

            services.AddTransient<IMovImunobiologicoRepository, MovImunobiologicoRepository>();
            services.AddTransient<IMovImunobiologicoCommand, MovImunobiologicoCommandText>();

            services.AddTransient<IProdutoRepository, ProdutoRepository>();
            services.AddTransient<IProdutoCommand, ProdutoCommandText>();

            services.AddTransient<IProdutorRepository, ProdutorRepository>();
            services.AddTransient<IProdutorCommand, ProdutorCommandText>();

            services.AddTransient<IReportRepository, ReportRepository>();
            services.AddTransient<IReportCommand, ReportCommandText>();

            services.AddTransient<IUnidadeMedRepository, UnidadeMedRepository>();
            services.AddTransient<IUnidadeMedCommand, UnidadeMedCommandText>();

            services.AddTransient<IVacinaApresentacaoRepository, VacinaApresentacaoRepository>();
            services.AddTransient<IVacinaApresentacaoCommand, VacinaApresentacaoCommandText>();

            services.AddTransient<IViaAdmRepository, ViaAdmRepository>();
            services.AddTransient<IViaAdmCommand, ViaAdmCommandText>();
            #endregion

            #region Indicadores
            services.AddTransient<IIndicador1Repository, Indicador1Repository>();
            services.AddTransient<IIndicador1Command, Indicador1CommandText>();

            services.AddTransient<IIndicador2Repository, Indicador2Repository>();
            services.AddTransient<IIndicador2Command, Indicador2CommandText>();

            services.AddTransient<IIndicador2Repository, Indicador2Repository>();
            services.AddTransient<IIndicador2Command, Indicador2CommandText>();

            services.AddTransient<IIndicador3Repository, Indicador3Repository>();
            services.AddTransient<IIndicador3Command, Indicador3CommandText>();

            services.AddTransient<IIndicador4Repository, Indicador4Repository>();
            services.AddTransient<IIndicador4Command, Indicador4CommandText>();

            services.AddTransient<IIndicador5Repository, Indicador5Repository>();
            services.AddTransient<IIndicador5Command, Indicador5CommandText>();

            services.AddTransient<IIndicador6Repository, Indicador6Repository>();
            services.AddTransient<IIndicador6Command, Indicador6CommandText>();

            services.AddTransient<IIndicador7Repository, Indicador7Repository>();
            services.AddTransient<IIndicador7Command, Indicador7CommandText>();
            #endregion

            #region Segurança
            services.AddTransient<IPerfilRepository, PerfilRepository>();
            services.AddTransient<IPerfilCommand, PerfilCommandText>();

            services.AddTransient<IPerfilUsuarioRepository, PerfilUsuarioRepository>();
            services.AddTransient<IPerfilUsuarioCommand, PerfilUsuarioCommandText>();
            #endregion

            #region Prontuario
            services.AddTransient<IExameRepository, ExameRepository>();
            services.AddTransient<IExameCommand, ExameCommandText>();
            #endregion

            #endregion

            services.AddMvc().AddJsonOptions(opcoes =>
            {
                //configuração para que valores nulos não sejam retornados na consulta
                opcoes.SerializerSettings.NullValueHandling =
                    Newtonsoft.Json.NullValueHandling.Ignore;
            });

            //funciona
            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowAnyOrigin();
            corsBuilder.AllowCredentials();
            var lista = "x-total-count";
            corsBuilder.WithExposedHeaders(lista);
            services.AddCors(options =>
            {
                options.AddPolicy("SiteCorsPolicy", corsBuilder.Build());
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["jwt:key"])),
                ClockSkew = TimeSpan.Zero
            });

            services.AddScoped<PermissaoUsuarioFilter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseCors("SiteCorsPolicy");
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseMvc();
            RotativaConfiguration.Setup(env);
        }
    }
}
