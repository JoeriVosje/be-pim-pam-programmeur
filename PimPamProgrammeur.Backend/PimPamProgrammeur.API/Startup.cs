using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PimPamProgrammeurContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("PimPamProgrammeurConnection")));

            services.AddSingleton<JwtSecurityTokenHandler>();
            services.AddSingleton<ITokenProvider, TokenProvider>();

            // Repositories
            services.AddTransient<IModuleRepository, ModuleRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            // AutoMapper
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ModuleMapping());
                mc.AddProfile(new UserMapping());
                
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            // SmtpClient
            services.AddSingleton(p => ActivatorUtilities.CreateInstance<SmtpClient>(p, Constants.Smtp.MailServerAddress, Constants.Smtp.MailServerPort));
            services.AddSingleton<ISmtpService, SmtpService>();

            // Processor
            services.AddTransient<IModuleProcessor, ModuleProcessor>();
            services.AddTransient<IUserProcessor, UserProcessor>();

            // Validators
            services.AddSingleton<IValidator<ModuleRequestDto>, ModuleRequestDtoValidator>();
            services.AddTransient<IValidator<UserRequestDto>, UserRequestDtoValidator>();
            services.AddSingleton<IValidator<UserLoginRequestDto>, UserLoginRequestDtoValidator>();

            // controllers
            services.AddAuthentication(Constants.TokenAuthenticationScheme)
                .AddScheme<AuthenticationSchemeOptions, TokenAuthenticationHandler>(Constants.TokenAuthenticationScheme, o => { }); ;
            services.AddAuthorization(e =>
            {
                e.AddPolicy(Constants.Admin, builder => builder.RequireClaim(Constants.RoleId, new List<string> {"1"}));
                e.AddPolicy(Constants.Student, builder => builder.RequireClaim(Constants.RoleId, new List<string> {"0","1"}));
            });
            services.AddControllers();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ITokenProvider tokenProvider)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
