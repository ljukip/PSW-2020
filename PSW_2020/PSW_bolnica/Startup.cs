using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PSW_bolnica.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using PSW_bolnica.interfaces;
using PSW_bolnica.services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace PSW_bolnica
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
            services.AddControllers().AddNewtonsoftJson(options =>
               options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddDbContext<DBContext>(options =>
                     options.UseSqlServer(ConfigurationExtensions.GetConnectionString(Configuration, "DBContextConnectionString")).UseLazyLoadingProxies());
        
            services.AddRazorPages().WithRazorPagesRoot("/Pages");
            ConfigureAuthentication(services);

            //registraton of services used
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<IReferralService, ReferralService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseStatusCodePages(context =>
            {
                var response = context.HttpContext.Response;

                if (response.StatusCode == (int)System.Net.HttpStatusCode.Unauthorized)
                {
                    response.Redirect(string.Format("{0}://{1}{2}",
                        response.HttpContext.Request.Scheme,
                    response.HttpContext.Request.Host, "/login"));
                }
                return Task.CompletedTask;
            });
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCookiePolicy();

            //app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }

        private void ConfigureAuthentication(IServiceCollection services)
        {

            var key = Encoding.ASCII.GetBytes(Configuration["AppSettings:JWT:Secret_Key"]);

            services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
           .AddJwtBearer(config =>
           {
               config.RequireHttpsMetadata = false;
               config.SaveToken = true;
               config.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(key),
                   ValidateIssuer = false,
                   ValidateAudience = false
               };

               config.Events = new JwtBearerEvents()
               {
                   OnMessageReceived = context =>
                   {

                       if (context.Request.Cookies.TryGetValue("JWT", out string token))
                       {
                           context.Token = token;
                       }
                       return Task.CompletedTask;
                   },
               };


           });


        }

    }
}
