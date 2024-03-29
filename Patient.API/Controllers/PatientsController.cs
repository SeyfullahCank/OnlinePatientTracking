using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessageBroker.Base.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Patient.API.Data;
using Patient.API.Events;
using Patient.API.Models;

namespace Patient.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly PatientAPIContext _context;
        private readonly IMessageBroker _messageBroker;

        public PatientsController(PatientAPIContext context, IMessageBroker messageBroker)
        {
            _context = context;
            _messageBroker = messageBroker;
        }

        // GET: api/Patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Patient>>> GetPatient()
        {
            return await _context.Patient.ToListAsync();
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Patient>> GetPatient(Guid id)
        {
            var patient = await _context.Patient.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            return patient;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatient(Guid id, Models.Patient patient)
        {
            if (id != patient.Id)
            {
                return BadRequest();
            }

            _context.Entry(patient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
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

        // POST: api/Patients
        [HttpPost]
        public async Task<ActionResult<Models.Patient>> PostPatient(Models.Patient patient)
        {
            patient.SetId();
            _context.Patient.Add(patient);
            await _context.SaveChangesAsync();

            _messageBroker.Publish(new PatientCreatedEvent(patient.Id, patient.FullName));

            return CreatedAtAction("GetPatient", new { id = patient.Id }, patient);
        }

        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(Guid id)
        {
            var patient = await _context.Patient.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            _context.Patient.Remove(patient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PatientExists(Guid id)
        {
            return _context.Patient.Any(e => e.Id == id);
        }
    }
}
