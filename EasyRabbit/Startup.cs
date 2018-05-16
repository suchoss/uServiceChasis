using EasyRabbit.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Orion.Rabbit;
using RabbitData;

namespace EasyRabbit
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
            // Create in memory database context
            // db context is scoped service - it is shitty idea to make it singleton!
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer("Server=localhost; Database=RabbitTest; MultipleActiveResultSets=true; User ID=sa; Password=Admin1234");
            });

            services.AddMvc();

            // RabbitMQ
            services.Configure<RabbitConfig>(Configuration.GetSection("RabbitConfig"));
            services.AddSingleton<RabbitConnector>();

            services.AddSingleton<IHostedService, GenericHostedSubscriber<CalculatorInputs>>();
            services.AddScoped<IScopedProcessingService<CalculatorInputs>, RabbitSubscribers.Adder>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
