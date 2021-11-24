using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LessonLog.Domain;
using LessonLog.Infrastructure;
using LessonLog.Infrastructure.Repository;

namespace LessonLog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendancesController : ControllerBase
    {
        private readonly LessonLogContext _context;
        private readonly AttendanceRepository _attendanceRepository;

        public AttendancesController(LessonLogContext context)
        {
            _context = context;
            _attendanceRepository = new AttendanceRepository(_context);
        }

        // GET: api/Attendances
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attendance>>> GetAttendences()
        {
            //return await _context.Attendences.ToListAsync();
            return await _attendanceRepository.GetAllAttendencesAsync();
        }

        // GET: api/Attendances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Attendance>> GetAttendance(Guid id)
        {
            //var attendance = await _context.Attendences.FindAsync(id);
            var attendance = await _attendanceRepository.GetAttendanceAsync(id);

            if (attendance == null)
            {
                return NotFound();
            }

            return attendance;
        }

        // PUT: api/Attendances/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttendance(Guid id, Attendance attendance)
        {
            if (id != attendance.Id)
            {
                return BadRequest();
            }

            await _attendanceRepository.UpdateAttendenceAsync(attendance);

            return NoContent();
            /*_context.Entry(attendance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendanceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();*/
        }

        // POST: api/Attendances
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Attendance>> PostAttendance(Attendance attendance)
        {
            //_context.Attendences.Add(attendance);
            //await _context.SaveChangesAsync();
            await _attendanceRepository.AddAttendenceAsync(attendance);

            return CreatedAtAction("GetAttendance", new { id = attendance.Id }, attendance);
        }

        // DELETE: api/Attendances/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttendance(Guid id)
        {
            //var attendance = await _context.Attendences.FindAsync(id);

            if (attendance == null)
            {
                return NotFound();
            }

            _context.Attendences.Remove(attendance);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AttendanceExists(Guid id)
        {
            return _context.Attendences.Any(e => e.Id == id);
        }
    }
}
