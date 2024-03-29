using MessageBroker.Base.Abstract;
using Notification.API.Data;

namespace Notification.API.Events
{
    public class AppointmentCreatedEventHandler : IEventHandler<AppointmentCreatedEvent>
    {
        private readonly NotificationAPIContext _context;

        public AppointmentCreatedEventHandler(NotificationAPIContext context)
        {
            _context = context;
        }
        public async Task Handle(AppointmentCreatedEvent @event)
        {
            var patient = _context.Patients.FirstOrDefault(x=>x.Id == @event.PatientId);
            string message = "Sayın " + patient.FullName +  " randevunuz oluşturulmuştur. Randevu tarihi : " + @event.AppointmentDate;
            var notification = new Models.Notification(@event.PatientId, message);

            _context.Notification.Add(notification);
            await _context.SaveChangesAsync();
        }
    }
}
