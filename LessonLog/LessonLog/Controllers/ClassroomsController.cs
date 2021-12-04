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
    public class ClassroomController : ControllerBase
    {
        private readonly LessonLogContext _context;
        private readonly ClassroomRepository _classroomRepository;

        public ClassroomController(LessonLogContext context)
        {
            _context = context;
            _classroomRepository = new ClassroomRepository(_context);
        }

        // GET: api/Lessons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Classroom>>> GetLessons()
        {
            return await _classroomRepository.GetAllAsync();
        }

        // GET: api/Lessons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClassroomOutDTO>> GetLesson(Guid id)
        {
            var classroom = await _classroomRepository.GetByIdAsync(id);

            if (classroom == null)
            {
                return NotFound();
            }

            return classroom;
        }

        // PUT: api/Lessons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLesson(Guid id, ClassroomOutDTO classroom)
        {
            if (id != classroom.Id)
            {
                return BadRequest();
            }

            await _classroomRepository.UpdateAsync(classroom);

            return NoContent();
        }

        // POST: api/Lessons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClassroomOutDTO>> PostLesson(ClassroomDTO classroom)
        {
            Guid id = await _classroomRepository.AddAsync(classroom);

            return CreatedAtAction("GetClassroom", new { id = id }, classroom);
        }

        // DELETE: api/Lessons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLesson(Guid id)
        {
            var classroom = await _classroomRepository.GetByIdAsync(id);

            if (classroom == null)
            {
                return NotFound();
            }

            await _classroomRepository.DeleteAsync(id);

            return NoContent();
        }

        private bool ClassroomExists(Guid id)
        {
            return _context.Classrooms.Any(e => e.Id == id);
        }
    }
}
