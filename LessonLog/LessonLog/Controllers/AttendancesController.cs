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
            return await _attendanceRepository.GetAllAsync();
        }

        // GET: api/Attendances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AttendanceOutDTO>> GetAttendance(Guid id)
        {
            //var attendance = await _context.Attendences.FindAsync(id);
            var attendance = await _attendanceRepository.GetByIdAsync(id);

            if (attendance == null)
            {
                return NotFound();
            }
            AttendanceOutDTO dto = new AttendanceOutDTO()
            {
                Id = attendance.Id,
                Status = attendance.Status,
                Late = attendance.Comming,
                Leaving = attendance.Leaving,
                LessonId = attendance.LessonId
            };

            return dto;
        }

        // PUT: api/Attendances/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttendance(Guid id, AttendanceOutDTO attendance)
        {
            if (id != attendance.Id)
            {
                return BadRequest();
            }

            await _attendanceRepository.UpdateAsync(attendance);

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
        public async Task<ActionResult<AttendanceOutDTO>> PostAttendance(AttendanceDTO attendanceDTO)
        {
            //_context.Attendences.Add(attendance);
            //await _context.SaveChangesAsync();
            var attendance = new Attendance()
            {
                Status = attendanceDTO.Status,
                Comming = attendanceDTO.Comming,
                Leaving = attendanceDTO.Leaving,
                LessonId = attendanceDTO.LessonId,
                PresenceTime = attendanceDTO.Leaving.Subtract(attendanceDTO.Comming),
                PresencePercentage = (attendanceDTO.Leaving.Subtract(attendanceDTO.Comming).Minutes / 90) * 100
            };
            Guid id = await _attendanceRepository.AddAsync(attendance);

            return CreatedAtAction("GetAttendance", new { id = id }, attendance);
        }

        // DELETE: api/Attendances/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttendance(Guid id)
        {
            //var attendance = await _context.Attendences.FindAsync(id);
            var attendance = await _attendanceRepository.GetByIdAsync(id);

            if (attendance == null)
            {
                return NotFound();
            }

            //_context.Attendences.Remove(attendance);
            //await _context.SaveChangesAsync();
            await _attendanceRepository.DeleteAsync(id);

            return NoContent();
        }

        private bool AttendanceExists(Guid id)
        {
            return _context.Attendences.Any(e => e.Id == id);
        }
    }
}
