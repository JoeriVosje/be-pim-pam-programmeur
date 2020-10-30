using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PimPamProgrammeur.API.Mapping;
using PimPamProgrammeur.API.Processors;
using PimPamProgrammeur.Data;
using PimPamProgrammeur.Dto;
using PimPamProgrammeur.Dto.Validator;
using PimPamProgrammeur.Model;
using PimPamProgrammeur.Repository;

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

            // Processor
            services.AddTransient<IModuleProcessor, ModuleProcessor>();
            services.AddTransient<IUserProcessor, UserProcessor>();

            // Validators
            services.AddSingleton<IValidator<ModuleRequestDto>, ModuleRequestDtoValidator>();
            services.AddTransient<IValidator<UserRequestDto>, UserRequestDtoValidator>();
            services.AddSingleton<IValidator<UserLoginRequestDto>, UserLoginRequestDtoValidator>();

            // controllers
            services.AddControllers();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
