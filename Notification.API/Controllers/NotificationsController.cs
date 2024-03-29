using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notification.API.Data;

namespace Notification.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly NotificationAPIContext _context;

        public NotificationsController(NotificationAPIContext context)
        {
            _context = context;
        }

        // GET: api/Notifications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Notification>>> GetNotification()
        {
            return await _context.Notification.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Notification>> GetNotification(Guid id)
        {
            var notification = await _context.Notification.FindAsync(id);

            if (notification == null)
            {
                return NotFound();
            }

            return notification;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotification(Guid id, Models.Notification notification)
        {
            if (id != notification.Id)
            {
                return BadRequest();
            }

            _context.Entry(notification).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificationExists(id))
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

        // POST: api/Notifications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Models.Notification>> PostNotification(Models.Notification notification)
        {
            _context.Notification.Add(notification);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNotification", new { id = notification.Id }, notification);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(Guid id)
        {
            var notification = await _context.Notification.FindAsync(id);
            if (notification == null)
            {
                return NotFound();
            }

            _context.Notification.Remove(notification);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NotificationExists(Guid id)
        {
            return _context.Notification.Any(e => e.Id == id);
        }
    }
}
