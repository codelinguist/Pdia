using CoreInfrastructure;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdia.Data
{
    public class DataLayerModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IUnitOfWork>().To<PdiaUnitOfWork>();
        }
    }
}
