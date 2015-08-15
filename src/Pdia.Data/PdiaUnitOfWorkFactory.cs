using Pdia.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdia.Data
{
    public class PdiaUnitOfWorkFactory : IPdiaUnitOfWorkFactory
    {
        public IPdiaUnitOfWork Create()
        {
            return new PdiaUnitOfWork();
        }
    }
}
