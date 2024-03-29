using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Patient.API.Models;

namespace Patient.API.Data
{
    public class PatientAPIContext : DbContext
    {
        public PatientAPIContext (DbContextOptions<PatientAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Patient.API.Models.Patient> Patient { get; set; } = default!;
    }
}
