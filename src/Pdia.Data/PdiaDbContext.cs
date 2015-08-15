using Pdia.Entities;
using System.Data.Entity;

namespace Pdia.Data
{
    public class PdiaDbContext:DbContext
    {
        public PdiaDbContext():base("DefaultConnection")
        {

        }

        public DbSet<Child> Children { get; set; }
        //public DbSet<Patient> Patients { get; set; }

        public DbSet<UserAccount> UserAccounts { get; set; }
    }
}
