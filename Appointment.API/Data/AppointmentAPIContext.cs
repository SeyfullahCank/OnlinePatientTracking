using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Appointment.API.Models;

namespace Appointment.API.Data
{
    public class AppointmentAPIContext : DbContext
    {
        public AppointmentAPIContext (DbContextOptions<AppointmentAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Appointment> Appointment { get; set; } = default!;
    }
}
