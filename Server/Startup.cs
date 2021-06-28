using DataStorage.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Server
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
            services.AddControllers();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new() {Title = "Server", Version = "v1"}); });

            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("Postgres"), b => b.MigrationsAssembly("Server"));
            });
            services.AddCors(options =>
            {
                options.AddPolicy("AllAllowed",
                    builder => { builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod(); });
                options.AddPolicy("Heroku",
                    builder => { builder.WithOrigins("abtestproject.heroku.com").AllowAnyHeader().AllowAnyMethod(); });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationContext ctx)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Server v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(env.IsDevelopment() ? "AllAllowed" : "Heroku");

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            ctx.Database.EnsureCreated();
        }
    }
}