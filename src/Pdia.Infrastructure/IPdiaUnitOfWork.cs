using CoreInfrastructure;
using Pdia.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdia.Infrastructure
{
    public interface IPdiaUnitOfWork:IUnitOfWork
    {
        IRepository<AppClaim> AppClaimRepository { get; }
        IRepository<BabyBook> BabyBookRepository { get; }
        IRepository<Child> ChildRepository { get; }
        IRepository<Clinic> ClinicRepository { get; }
        IRepository<Page> PageRepository { get; }
        IRepository<Patient> PatientRepository { get; }
        IRepository<Pediatrician> PediatricianRepository { get; }
        IRepository<Post> PostRepository { get; }
        IRepository<UserProfile> UserProfileRepository { get; }
    }
}
