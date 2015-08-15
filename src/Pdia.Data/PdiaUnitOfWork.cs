using Pdia.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreInfrastructure;
using Pdia.Entities;

namespace Pdia.Data
{
    public class PdiaUnitOfWork : IPdiaUnitOfWork
    {
        public IRepository<Child> ChildRepository
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<Patient> PatientRepository
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<Pediatrician> PediatricianRepository
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
