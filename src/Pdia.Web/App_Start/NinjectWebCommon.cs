[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Pdia.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Pdia.Web.App_Start.NinjectWebCommon), "Stop")]

namespace Pdia.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using CoreArchitecture.Mvc;
    using Pdia.Data;
    using Pdia.Services;

    public static class NinjectWebCommon
    {
        private static readonly MvcBootstrapper bootstrapper = new MvcBootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                WebModule.Kernel = kernel;
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => bootstrapper.Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Load(typeof(DataLayerModule).Assembly);
            kernel.Load(typeof(ServiceLayerModule).Assembly);
            kernel.Load(typeof(WebModule).Assembly);
        }
    }
}
