using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreInfrastructure
{
    /// <summary>
    /// Contains Application Global Objects and States
    /// </summary>
    public class AppInfrastructure
    {
        public static IKernel GlobalKernel { get; private set; }
        public static void Initialize(IKernel globalKernel)
        {
            GlobalKernel = globalKernel;
        }
    }
}
