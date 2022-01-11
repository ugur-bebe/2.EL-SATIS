using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using VTYS_PROJE.Business.Concrete;
using VTYS_PROJE.Core.LogManager;
using VTYS_PROJE.Business.Abstract;
using VTYS_PROJE.DAL.Abstarct;
using VTYS_PROJE.DAL.Concrete;

namespace VTY_PROJE
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

            services.AddSingleton<IProductService, ProductService>();
            services.AddSingleton<IProductDal, ProductDal>();

            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IUserDal, UserDal>();

            services.AddSingleton<ICategoryService, CategoryService>();
            services.AddSingleton<ICategoryDal, CategoryDal>();

            services.AddSingleton<IUserTypeService, UserTypeService>();
            services.AddSingleton<IUserTypeDal, UserTypeDal>();

            services.AddSingleton<IAddressService, AddressService>();
            services.AddSingleton<IAddressDal, AddressDal>();

            services.AddSingleton<IProductTypeService, ProductTypeService>();
            services.AddSingleton<IProductTypeDal, ProductTypeDal>();

            services.AddSingleton<ICityService, CityService>();
            services.AddSingleton<ICityDal, CityDal>();

            services.AddSingleton<IDistrictService, DistrictService>();
            services.AddSingleton<IDistrictDal, DistrictDal>();

            services.AddSingleton<INoSqlService, NoSqlService>();
            services.AddSingleton<INoSqlDal, NoSqlDal>();


            services.AddSingleton<ILogManager, LogManager>();


            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddSession();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(x =>
                {
                    x.LoginPath = "/login";
                });

            services.AddControllersWithViews();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "VTY_PROJE", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "VTY_PROJE v1"));
            }

            app.UseSession();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseCookiePolicy();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Account}/{action=login}");
            });
        }
    }
}
