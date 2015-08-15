using Ninject.Infrastructure;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using CoreArchitecture;
using Ninject.Syntax;
using CoreInfrastructure.EntityFramework;
using CoreInfrastructure;

namespace CoreArchitecture.Mvc
{
    /// <summary>
    /// Bootstraps common dependency for a web application (ASP.Net MVC or Web API
    /// </summary>
    public class MvcBootstrapper : IBootstrapper, IHaveKernel
    {
        public IKernel Kernel
        {
            get
            {
                return _bootstrapper.Kernel;
            }
        }
        //Internal bootstrap implementor
        public Bootstrapper _bootstrapper;

        public void Initialize(Func<IKernel> createKernelCallback)
        {
            //Need to automatically Bind EntityFrameworkQueryAsyncProxy to IAsyncQueryProxy which will be used in async-friendly LINQ queries
            Func<IKernel> callbackOverride = () => {
                var kernel = createKernelCallback.Invoke();
                if (kernel is BindingRoot)
                {
                    (kernel as BindingRoot).Bind<IAsyncQueryProxy>().To<EntityFrameworkQueryAsyncProxy>();
                    AppInfrastructure.Initialize(kernel);
                }
                else
                {
                    throw new NotSupportedException("The kernel created does not implement the IBindingSyntax");
                }
                return kernel;
            };
            _bootstrapper.Initialize(callbackOverride);
        }

        public void InitializeHttpApplication(System.Web.HttpApplication httpApplication)
        {
            _bootstrapper.InitializeHttpApplication(httpApplication);
        }

        public void ShutDown()
        {
            _bootstrapper.ShutDown();

        }
    }
    
}
