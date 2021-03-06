﻿using Pdia.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdia.Infrastructure
{
    public interface IUserProfileService
    {

        Task<Child> GetChildProfileAsync(Guid pediaId);
        Task<UserProfile> GetPediaProfileAsync(Guid pediaId);//TODO: should include 
        Task<UserProfile> GetParentAsync();
        Task<List<Child>> GetChildrenAsync();
    }
}
