using Pdia.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreInfrastructure;
using Pdia.Entities;
using CoreInfrastructure.EntityFramework;

namespace Pdia.Data
{
    public class PdiaUnitOfWork : IPdiaUnitOfWork
    {
        private PdiaDbContext _dbContext;
        public PdiaUnitOfWork()
        {
            _dbContext = new PdiaDbContext();
        }
        public IRepository<Child> _childRepository;
        public IRepository<Child> ChildRepository
        {
            get
            {
                if (_childRepository == null)
                {
                    _childRepository = new EntityRepository<Child>(_dbContext);
                }
                return _childRepository;
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
