﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreInfrastructure
{
    public interface IUnitOfWorkFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IUnitOfWork Create();
    }
}
