using Microsoft.EntityFrameworkCore;
using Notification.API.Models;

namespace Notification.API.Data
{
    public class NotificationAPIContext : DbContext
    {
        public NotificationAPIContext (DbContextOptions<NotificationAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Notification> Notification { get; set; } = default!;
        public DbSet<Patient> Patients { get; set; } = default!;
    }
}
