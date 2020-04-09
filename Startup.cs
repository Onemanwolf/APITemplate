using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using ReservationApi.Models;
using ReservationApi.Repos;
using ReservationApi.Services;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using System.Linq;

namespace ReservationApi
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
            //Session 1
            services.AddControllers().AddJsonOptions(options => { options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;});

            services.AddSwaggerGen(options =>
            {


                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Reservation Api", Version = "v1" });
                options.AddSecurityDefinition("Bearer",
                  new OpenApiSecurityScheme
                  {
                      In = ParameterLocation.Header,
                      Description = "Please enter into field the word 'Bearer' following by space and JWT",
                      Name = "Authorization",
                      Type = SecuritySchemeType.ApiKey
                  });
               // options.AddSecurityRequirement();
            });


            //Session 3
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 // base-address of your identityserver
                 options.Authority = "https://localhost:5001/";

                 // name of the API resource
                 options.Audience = "reservationapi";
             });

            //Session 1
            //The configuration instance to which the appsettings.json file's BookstoreDatabaseSettings section binds is 
            //registered in the Dependency Injection (DI) container. For example, a BookstoreDatabaseSettings object's 
            //ConnectionString property is populated with the BookstoreDatabaseSettings:ConnectionString property in 
            //appsettings.json.
            //The IBookstoreDatabaseSettings interface is registered in DI with a singleton service lifetime.When 
            //injected, the interface instance resolves to a BookstoreDatabaseSettings object.

            //configure mongodb connection and register an instance of IMongoCollection for use in service
            services.AddScoped(sp =>
            {
                var config = Configuration.GetSection("ReservationDataBaseSettings");

                var client = new MongoClient(config.GetValue<string>("ConnectionString"));
                var database = client.GetDatabase(config.GetValue<string>("DatabaseName"));

                return database.GetCollection<Reservation>(config.GetValue<string>("ReservationCollectionName"));
            });

            services.AddScoped<ReservationService>();

            services.AddScoped<IRepository<Reservation>, MongoRepository<Reservation>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {


                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //Session 2
            app.UseSwagger();


            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Reservation Api");

                //c.DocExpansion(DocExpansion.None);

                c.RoutePrefix = string.Empty;
            });




            // Session 3
            app.UseAuthentication();



            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
