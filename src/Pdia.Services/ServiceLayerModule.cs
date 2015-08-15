using Ninject.Modules;
using Pdia.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdia.Services
{
    public class ServiceLayerModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IAuthorizationService>().To<AuthorizationService>().InSingletonScope();
            this.Bind<IConnectionService>().To<ConnectionService>().InSingletonScope();
        }
    }
}
