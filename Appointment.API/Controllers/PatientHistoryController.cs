using Appointment.API.Data;
using Appointment.API.RequestDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Appointment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientHistoryController : ControllerBase
    {
        private readonly AppointmentAPIContext _context;

        public PatientHistoryController(AppointmentAPIContext context)
        {
            _context = context;
        }

        /// GET All appointments api/<PatientHistoryController>/patientId
        [HttpGet("{patientId}")]
        public async Task<ActionResult<IEnumerable<Models.Appointment>>> GetAppointmentsByPatientId(Guid patientId)
        {
            var appointmentList = await _context.Appointment.Where(a => a.PatientId == patientId).ToListAsync();

            if (appointmentList == null)
            {
                return NotFound();
            }

            return appointmentList;
        }

        /// GET filtered appointments api/<PatientHistoryController>/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Appointment>>> GetAppointmentsByPatientId(ViewPatientHistoryRequestDto requestDto)
        {
            var predicate = PredicateBuilder.True<Models.Appointment>();

            predicate = predicate.And(a => a.PatientId == requestDto.PatientId);

            if (requestDto.AppointmentDate.HasValue)
                predicate = predicate.And(a => a.AppointmentDate == requestDto.AppointmentDate.Value);

            if (requestDto.DepartmentId.HasValue && requestDto.DepartmentId != Guid.Empty)
                predicate = predicate.And(a => a.DepartmentId == requestDto.DepartmentId.Value);

            if (requestDto.DoctorId.HasValue && requestDto.DoctorId != Guid.Empty)
                predicate = predicate.And(a => a.DoctorId == requestDto.DoctorId.Value);

            var appointmentList = await _context.Appointment.Where(predicate).ToListAsync();

            if (appointmentList == null)
                return NotFound();

            return appointmentList;
        }

    }
}
