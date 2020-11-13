using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;

using AutoMapper;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using PimPamProgrammeur.API.Mapping;
using PimPamProgrammeur.API.Middleware;
using PimPamProgrammeur.API.Processors;
using PimPamProgrammeur.Data;
using PimPamProgrammeur.Dto;
using PimPamProgrammeur.Dto.Validator;
using PimPamProgrammeur.Model;
using PimPamProgrammeur.Repository;
using PimPamProgrammeur.Utils;

namespace PimPamProgrammeur.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy(Constants.AllowedCorsPolicies, builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));


            services.AddDbContext<PimPamProgrammeurContext>(opt => opt.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("PimPamProgrammeurConnection")));

            services.AddSingleton<JwtSecurityTokenHandler>();
            services.AddSingleton<ITokenProvider, TokenProvider>();

            // Repositories
            services.AddTransient<IModuleRepository, ModuleRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IClassroomRepository, ClassroomRepository>();
            services.AddTransient<ISessionRepository, SessionRepository>();
            services.AddTransient<IResultRepository, ResultRepository>();
            services.AddTransient<IComponentRepository, ComponentRepository>();
            services.AddTransient<IAnswerRepository, AnswerRepository>();

            // AutoMapper
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ModuleMapping());
                mc.AddProfile(new UserMapping());
                mc.AddProfile(new ClassroomMapping());
                mc.AddProfile(new SessionMapping());
                mc.AddProfile(new ResultMapping());
                mc.AddProfile(new ComponentMapping());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            // SmtpClient
            services.AddSingleton(p => ActivatorUtilities.CreateInstance<SmtpClient>(p, Constants.Smtp.MailServerAddress, Constants.Smtp.MailServerPort));
            services.AddSingleton<ISmtpService, SmtpService>();

            // HashingService
            services.AddSingleton<IHashingService, HashingService>();

            // PasswordGeneratorService
            services.AddSingleton<IPasswordGeneratorService, PasswordGeneratorService>();

            // Processor
            services.AddTransient<IModuleProcessor, ModuleProcessor>();
            services.AddTransient<IUserProcessor, UserProcessor>();
            services.AddTransient<IClassroomProcessor, ClassroomProcessor>();
            services.AddTransient<ISessionProcessor, SessionProcessor>();
            services.AddTransient<IResultProcessor, ResultProcessor>();
            services.AddTransient<IComponentProcessor, ComponentProcessor>();

            // Validators
            services.AddSingleton<IValidator<ModuleRequestDto>, ModuleRequestDtoValidator>();
            services.AddTransient<IValidator<UserRequestDto>, UserRequestDtoValidator>();
            services.AddSingleton<IValidator<UserLoginRequestDto>, UserLoginRequestDtoValidator>();
            services.AddSingleton<IValidator<ClassroomRequestDto>, ClassroomRequestDtoValidator>();
            services.AddSingleton<IValidator<ModuleUpdateRequestDto>, ModuleUpdateRequestDtoValidator>();
            services.AddTransient<IValidator<ResultRequestDto>, ResultRequestDtoValidator>();
            services.AddTransient<IValidator<ComponentOrderRequestDto>, ComponentOrderRequestDtoValidator>();
            services.AddSingleton<IValidator<ComponentRequestDto>, ComponentRequestDtoValidator>();
            services.AddSingleton<IValidator<ComponentUpdateRequestDto>, ComponentUpdateRequestDtoValidator>();
            services.AddSingleton<SessionRequestDtoValidator>();
            services.AddTransient<OpenSessionRequestDtoValidator>();
            services.AddTransient<CloseSessionRequestDtoValidator>();


            // controllers
            services.AddAuthentication(Constants.TokenAuthenticationScheme)
                .AddScheme<AuthenticationSchemeOptions, TokenAuthenticationHandler>(Constants.TokenAuthenticationScheme, o => { });
            services.AddAuthorization(e =>
            {
                e.AddPolicy(Constants.Admin, builder => builder.RequireClaim(Constants.RoleId, new List<string> { "1" }));
                e.AddPolicy(Constants.Student, builder => builder.RequireClaim(Constants.RoleId, new List<string> { "0", "1" }));
            });
            services.AddControllers();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ITokenProvider tokenProvider)
        {
            UpdateDatabase(app);

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseDeveloperExceptionPage();


            app.UseHttpsRedirection();

            app.UseRouting();
            // UseCors needs to be placed after UseRouting, but before UseAuthorization
            app.UseCors(Constants.AllowedCorsPolicies);
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<PimPamProgrammeurContext>())
                {
                    //context.Database.EnsureCreated();
                    context.Database.Migrate();
                }
            }
        }
    }
}
