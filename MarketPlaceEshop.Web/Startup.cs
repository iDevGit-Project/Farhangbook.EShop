using GoogleReCaptcha.V3;
using GoogleReCaptcha.V3.Interface;
using MarketPlaceEshop.Application.Services.ImplementationsServices;
using MarketPlaceEshop.Application.Services.Interfaces;
using MarketPlaceEshop.Common.BaseClasses;
using MarketPlaceEshop.DataAccessLayer.Context;
using MarketPlaceEshop.DataAccessLayer.Interfaces;
using MarketPlaceEshop.DataAccessLayer.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NToastNotify;
using System;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace MarketPlaceEshop.Web
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
            #region تنظیمات سرویس های برنامه
            services.AddControllersWithViews();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserService, UserServices>();
            services.AddScoped<ISiteService, SiteService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<ISmsService, SmsService>();
            services.AddScoped<IHashPasswordHelper, HashPasswordHelper>();
            services.AddHttpClient<ICaptchaValidator, GoogleReCaptchaValidator>();
            // Persian DateTime Services
            services.AddTransient<ConvertDate>();
            #endregion

            #region تنظیمات رشته اتصال به پایگاه داده
            services.AddDbContext<MarketPlaceEshopDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("StrConfigurationDatabase"));
            });
            #endregion

            #region تنظیمات مربوط به احراز هویت

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.LoginPath = "/login";
                options.LogoutPath = "/logout";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(43200);
            });

            #endregion

            #region و انکود کردن آنها HTML تنظیمات مربوط به کد های

            services.AddSingleton<HtmlEncoder>(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin, UnicodeRanges.Arabic }));

            #endregion

            #region متد های مربوط به نوتیفیکیشن
            services.AddMvc().AddNToastNotifyNoty(new NotyOptions
            {
                ProgressBar = true,
                Timeout = 5000,
                Theme = "mint"
            });

            //Or Simply go
            services.AddMvc().AddNToastNotifyNoty();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseNToastNotify();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllerRoute(
                //  name: "areas",
                //  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                //);

                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //app.UseHttpsRedirection();
            //app.UseStaticFiles();
            //app.UseSession();
            //app.UseRouting();
            //app.UseAuthentication();
            //app.UseAuthorization();

            //app.UseEndpoints(routes =>
            //{
            //    routes.MapControllerRoute(
            //      name: "areas",
            //      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
            //    );
            //    routes.MapControllerRoute(
            //     name: "default",
            //     pattern: "{area=Admin}/{controller=Home}/{action=Index}/{id?}"
            //   );
            //});
        }
    }
}
