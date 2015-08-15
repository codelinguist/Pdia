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

        public IRepository<BabyBook> _babyBookRepository;
        public IRepository<Child> _childRepository;
        public IRepository<Clinic> _clinicRepository;
        public IRepository<Page> _pageRepository;
        public IRepository<Patient> _patientRepositry;
        public IRepository<Pediatrician> _pediatricianRepository;
        public IRepository<Post> _postRepository;
        public IRepository<UserProfile> _userProfileRepository;

        public IRepository<BabyBook> BabyBookRepository
        {
            get
            {
                if (_babyBookRepository == null)
                {
                    _babyBookRepository = new EntityRepository<BabyBook>(_dbContext);
                }
                return _babyBookRepository;
            }
        }

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

        public IRepository<Clinic> ClinicRepository
        {
            get
            {
                if (_clinicRepository == null)
                {
                    _clinicRepository = new EntityRepository<Clinic>(_dbContext);
                }
                return _clinicRepository;
            }
        }

        public IRepository<Page> PageRepository
        {
            get
            {
                if (_pageRepository == null)
                {
                    _pageRepository = new EntityRepository<Page>(_dbContext);
                }
                return _pageRepository;
            }
        }

        public IRepository<Patient> PatientRepository
        {
            get
            {
                if (_patientRepositry == null)
                {
                    _patientRepositry = new EntityRepository<Patient>(_dbContext);
                }
                return _patientRepositry;
            }
        }

        public IRepository<Pediatrician> PediatricianRepository
        {
            get
            {
                if (_pediatricianRepository == null)
                {
                    _pediatricianRepository = new EntityRepository<Pediatrician>(_dbContext);
                }
                return _pediatricianRepository;
            }
        }

        public IRepository<Post> PostRepository
        {
            get
            {
                if (_postRepository == null)
                {
                    _postRepository = new EntityRepository<Post>(_dbContext);
                }
                return _postRepository;
            }
        }

        public IRepository<UserProfile> UserProfileRepository
        {
            get
            {
                if (_userProfileRepository == null)
                {
                    _userProfileRepository = new EntityRepository<UserProfile>(_dbContext);
                }
                return _userProfileRepository;
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
