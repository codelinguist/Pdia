using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pdia.WebApi
{
    public class ApiModule : NinjectModule
    {
        public static IKernel Kernel { get; set; }
        public override void Load()
        {

        }
    }
}