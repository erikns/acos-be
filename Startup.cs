﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ACOS_be.Business;
using ACOS_be.Data;
using ACOS_be.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ACOS_be
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
            services.AddMvc();
            services.AddDbContext<ApplicationContext>(options =>
                options.UseNpgsql("Server=localhost;Database=acos;Port=5432;Password=acos;Username=acos"));
            services.AddTransient(typeof(TaskService), typeof(TaskServiceImpl));
            services.AddTransient(typeof(UserService), typeof(UserServiceImpl));
            services.AddTransient(typeof(TaskTypeService), typeof(TaskTypeServiceImpl));
            services.AddTransient(typeof(Repository), typeof(ApplicationContext));

            services.AddTransient(typeof(EventsFactory), typeof(EventsFactoryImpl));
            services.AddTransient(typeof(ModuleRegistry));

            // Bootstrap the modules
            var provider = services.BuildServiceProvider();
            var registry = provider.GetRequiredService<ModuleRegistry>();
            registry.Register<UserNotificationModule>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(config => config.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseMvc();
        }
    }
}
