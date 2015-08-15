using Pdia.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdia.Data
{
    public class PdiaDbContext:DbContext
    {
        public PdiaDbContext():base("DefaultConnection")
        {

        }

        public DbSet<Child> Children { get; set; }
        public DbSet<Patient> Patients { get; set; }
    }
}
