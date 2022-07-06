using GymManager.AplicationServices.Attendance;
using GymManager.AplicationServices.EquipmentTypes;
using GymManager.AplicationServices.Members;
using GymManager.AplicationServices.MembershipTypes;
using GymManager.AplicationServices.Navigation;
using GymManager.Core.Attendance;
using GymManager.Core.EquipmentTypes;
using GymManager.Core.Members;
using GymManager.Core.MembershipTypes;
using GymManager.DataAccess;
using GymManager.DataAccess.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Wkhtmltopdf.NetCore;

namespace GymManager.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
           
            string connectionString = Configuration.GetConnectionString("Default");

            services.AddDbContext<GymManagerContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            services.AddDefaultIdentity<IdentityUser>(option => option.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<GymManagerContext>()
                .AddUserStore<UserStore<IdentityUser, IdentityRole, GymManagerContext>>();

            services.ConfigureApplicationCookie(options => options.LoginPath = "/Account/LogIn");
            

            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            //services.AddSingleton<IMembersAppService, MembersAppService>();
            services.AddTransient<IMenuAppService, MenuAppService>();

            services.AddTransient<IMembersAppService, MembersAppService>();
            services.AddTransient<IRepository<int, Member>, MembersRepository>();

            services.AddTransient<IMembershipTypesAppService, MembershipTypesAppService>();
            services.AddTransient<IRepository<int, MembershipType>, MembershipTypesRepository>();

            services.AddTransient<IEquipmentTypesAppService, EquipmentTypesAppService>();
            services.AddTransient<IRepository<int, EquipmentType>, EquipmentTypesRepository>();

            services.AddTransient<IAttendanceAppService, AttendanceAppService>();
            services.AddTransient<IRepository<int, Check>, AttendanceRepository>();

            services.AddSingleton(Log.Logger);
            

            //services.Configure<ApiBehaviorOptions>(options =>
            //{
            //    options.SuppressModelStateInvalidFilter = true;
            //});

            services.AddWkhtmltopdf();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var defaultDateCulture = "es-MX";
            var ci = new CultureInfo(defaultDateCulture);

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(ci),
                SupportedCultures = new List<CultureInfo>
                {
                    ci,
                },
                SupportedUICultures = new List<CultureInfo> 
                { 
                    ci, 
                }
            }); ;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseStaticFiles();
            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
