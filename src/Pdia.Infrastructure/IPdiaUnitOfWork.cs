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
        IRepository<Child> ChildRepository { get; set; }
        IRepository<Pediatrician> PediatricianRepository { get; set; }
        IRepository<Patient> PatientRepository { get; set; }
    }
}
