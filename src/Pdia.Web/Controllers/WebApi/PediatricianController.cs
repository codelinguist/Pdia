﻿using Pdia.Infrastructure;
using Pdia.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Pdia.Web.Controllers.WebApi
{
    [RequireToken]
    [RoutePrefix("api/pediatricians")]
    public class PediatricianController : ApiController
    {
        IPediatricianService _pediatricianService;

        public PediatricianController(IPediatricianService pediatricianService)
        {
            _pediatricianService = pediatricianService;
        }


    }
}
