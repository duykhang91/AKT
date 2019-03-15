using AKTTool.Common;
using AKTTool.Repository;
using AKTTool.Services;
using Autofac;
using System;
using System.Linq;

namespace AKTTool.Infrastructure
{
  public class AutofacConfig
  {
    public static void Register(ContainerBuilder builder)
    {
      // Unit of Work
      builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

      // Service Context
      builder.RegisterType<ApplicationContext>().AsSelf().InstancePerLifetimeScope();

      // A trick to load dependency assembly prior to do the registration
      // Just need to register for 1 class to let the assembly load, once it is loaded, 
      // it can then be used to scan for other classes and registered dynamically

      // Repositories
      builder.RegisterType<DoorRepository>().As<IDoorRepository>();

      // Services
      builder.RegisterType<DatabaseServices>().As<IDatabaseServices>();
      builder.RegisterGeneric(typeof(DataProvider<>)).As(typeof(IDataProvider<>));

      RegisterDependenciesByConvention(builder);
    }

    /// <summary>
    /// Registers dynamically all implementations with their interface
    /// </summary>
    private static void RegisterDependenciesByConvention(ContainerBuilder builder)
    {
      var neededAssemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.StartsWith("AKT")).ToArray();
      builder.RegisterAssemblyTypes(neededAssemblies)
          .AsImplementedInterfaces()
          .PreserveExistingDefaults();
    }
  }
}
