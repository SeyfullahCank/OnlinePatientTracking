using MessageBroker.Base.Abstract;
using Notification.API.Data;
using Notification.API.Models;

namespace Notification.API.Events
{
    public class PatientCreatedEventHandler : IEventHandler<PatientCreatedEvent>
    {
        private readonly NotificationAPIContext _context;

        public PatientCreatedEventHandler(NotificationAPIContext context)
        {
            _context = context;
        }
        public async Task Handle(PatientCreatedEvent @event)
        {
            var patient = new Patient(@event.PatientId, @event.FullName);
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }
    }
}
