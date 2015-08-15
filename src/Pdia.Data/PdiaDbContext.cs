using Pdia.Entities;
using System.Data.Entity;

namespace Pdia.Data
{
    public class PdiaDbContext:DbContext
    {
        public PdiaDbContext():base("DefaultConnection")
        {

        }

        public DbSet<AppClaim> AppClaims { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Pediatrician> Pediatricians { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Parenting> Parentings { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Child> Children { get; set; }
        public DbSet<BabyBook> BabyBooks { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}
