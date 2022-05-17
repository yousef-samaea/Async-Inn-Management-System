using AsyncInn.Data;
using AsyncInn.Models;
using AsyncInn.Models.Interfaces;
using AsyncInn.Models.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                // There are other options like this
            })
            //.AddEntityFrameworkStores<SchoolDbContext>();

            //services.AddTransient<IUserService, IdentityUserService>
            .AddEntityFrameworkStores<AsyncInnDbContext>();
            services.AddTransient<IUserService, IdentityUserService>();


            services.AddMvc();
            services.AddControllers();
            services.AddTransient<IAmenity, AmenityServieces>();
            services.AddTransient<IHotel, HotelServices>();
            services.AddTransient<IRoom, RoomServieces>();
            services.AddTransient<IHotelRoom, HotelRoomServieces>();

           // services.AddControllers().AddNewtonsoftJson(opt =>
           //opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddDbContext<AsyncInnDbContext>(options => {
                // Our DATABASE_URL from js days
                string connectionString = Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("V1", new OpenApiInfo()
                {
                    Title = "AsyncInn",
                    Version = "V1"
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sneaker API V");
            //    c.RoutePrefix = string.Empty;
            //});

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseSwagger(x => {
                x.RouteTemplate = "/api/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/api/V1/swagger.json", "Hotel_management_system");
                x.RoutePrefix = "";
            });



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });

                endpoints.MapGet("/hey", async context =>
                {
                    await context.Response.WriteAsync("bgb");
                });
            });
        }
    }
}
