using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using WeatherWatcher.Api.Factories;
using WeatherWatcher.Api.Services;
using WeatherWatcher.Api.Services.Contracts;
using WeatherWatcher.Models;
using WeatherWatcher.Services;
using WeatherWatcher.Services.Contracts;

namespace WeatherWatcher
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            var openWeatherConfig = Configuration.GetSection(OpenWeatherApiOptions.appOptions);
            services.Configure<OpenWeatherApiOptions>(openWeatherConfig);

            services.AddScoped<IForecastService, ForecastService>();
            services.AddScoped<ICalculationService, CalculationService>();
            services.AddScoped<IWeatherForecastFactory, WeatherForecastFactory>();
            services.AddTransient<IOpenWeatherService, OpenWeatherService>();
            services.AddTransient<IDeserializeService, DeserializeService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddHttpClient();
            //services.AddCors(options =>
            //{
            //    options.AddDefaultPolicy(
            //        builder =>
            //        {
            //            builder.WithOrigins("http://localhost:8080/", "http://localhost:8081/", "http://localhost:8082/");
            //        });
            //});

            services.AddCors();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WeatherForecast", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WeatherForecast v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
