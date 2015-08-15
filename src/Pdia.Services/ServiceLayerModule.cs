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
            this.Bind<IBabyBookService>().To<BabyBookService>().InSingletonScope();
            this.Bind<IChildService>().To<ChildService>().InSingletonScope();
            this.Bind<IClinicService>().To<ClinicService>().InSingletonScope();
            this.Bind<IConnectionService>().To<ConnectionService>().InSingletonScope();
            this.Bind<IPageService>().To<PageService>().InSingletonScope();
            this.Bind<IParentingService>().To<ParentingService>().InSingletonScope();
            this.Bind<IPatientService>().To<PatientService>().InSingletonScope();
            this.Bind<IPediatricianService>().To<PediatricianService>().InSingletonScope();
            this.Bind<IPostService>().To<PostService>().InSingletonScope();
            this.Bind<IUserAccountService>().To<UserAccountService>().InSingletonScope();
        }
    }
}
