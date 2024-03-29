using Appointment.API.Data;
using Appointment.API.Events;
using Appointment.API.Models;
using MessageBroker.Base.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Appointment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly AppointmentAPIContext _context;
        private IMessageBroker _messageBroker;

        public AppointmentsController(AppointmentAPIContext context, IMessageBroker messageBroker)
        {
            _context = context;
            _messageBroker = messageBroker;
        }

        // GET: api/Appointments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Appointment>>> GetAppointment()
        {
            return await _context.Appointment.ToListAsync();
        }

        // GET: api/Appointments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Appointment>> GetAppointment(Guid id)
        {
            var appointment = await _context.Appointment.FindAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            return appointment;
        }

        // PUT: api/Appointments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointment(Guid id, Models.Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return BadRequest();
            }

            _context.Entry(appointment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Appointments
        [HttpPost]
        public async Task<ActionResult<Models.Appointment>> PostAppointment(Models.Appointment appointment)
        {
            appointment.SetId();
            _context.Appointment.Add(appointment);
            await _context.SaveChangesAsync();

            PublishAppointmentCreatedEvent(appointment);

            return CreatedAtAction("GetAppointment", new { id = appointment.Id }, appointment);
        }

        private void PublishAppointmentCreatedEvent(Models.Appointment appointment)
        {
            _messageBroker.Publish(new AppointmentCreatedEvent(appointment.Id, appointment.AppointmentDate, appointment.DoctorId));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(Guid id)
        {
            var appointment = await _context.Appointment.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            _context.Appointment.Remove(appointment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AppointmentExists(Guid id)
        {
            return _context.Appointment.Any(e => e.Id == id);
        }
    }
}
