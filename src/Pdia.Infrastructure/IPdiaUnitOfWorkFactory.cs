﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdia.Infrastructure
{
    public interface IPdiaUnitOfWorkFactory
    {
        IPdiaUnitOfWork Create();
    }
}
