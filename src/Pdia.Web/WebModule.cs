using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pdia.Web
{
    public class WebModule : NinjectModule
    {
        public override void Load()
        {

        }

        public static IKernel Kernel { get; set; }
    }
}