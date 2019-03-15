using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AKT.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AKTTool.Database;
using System;
using AKTTool.Infrastructure;
using Autofac;
using Autofac.Extensions.DependencyInjection;

namespace AKT
{
  public class Startup
  {
    private IServiceProvider _serviceProvider;

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public IServiceProvider ConfigureServices(IServiceCollection services)
    {
      // Autofac Container
      var builder = new ContainerBuilder();

      // Autofac
      AutofacConfig.Register(builder);

      // AutoMapper
      //AutoMapperConfig.Register(builder);

      services.Configure<CookiePolicyOptions>(options =>
      {
        // This lambda determines whether user consent for non-essential cookies is needed for a given request.
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.None;
      });

      //Identity
      services.AddDbContext<ApplicationDbContext>(options =>
          options.UseNpgsql(
              Configuration.GetConnectionString("DefaultConnection")));
      services.AddDefaultIdentity<IdentityUser>()
          .AddEntityFrameworkStores<ApplicationDbContext>();

      // DbContext
      services.AddDbContext<AppDbContext>(options =>
           options.UseNpgsql(
               Configuration.GetConnectionString("DefaultConnection")));
      services.AddEntityFrameworkNpgsql()
          .AddDbContext<AppDbContext>()
          .BuildServiceProvider();

      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

      // Build Autofac Service Provider
      builder.Populate(services);
      var container = builder.Build();
      _serviceProvider = new AutofacServiceProvider(container);
      return _serviceProvider;
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, AppDbContext db)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseDatabaseErrorPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseCookiePolicy();

      app.UseAuthentication();

      app.UseMvc(routes =>
      {
        routes.MapRoute(
                  name: "default",
                  template: "{controller=Home}/{action=Index}/{id?}");
      });
    }
  }
}
