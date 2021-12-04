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
    public class TeachersController : ControllerBase
    {
        private readonly LessonLogContext _context;
        private readonly TeacherRepository _teacherRepository;

        public TeachersController(LessonLogContext context)
        {
            _context = context;
            _teacherRepository = new TeacherRepository(_context);
        }

        // GET: api/Lessons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetLessons()
        {
            return await _teacherRepository.GetAllAsync();
        }

        // GET: api/Lessons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherOutDTO>> GetLesson(Guid id)
        {
            var teacher = await _teacherRepository.GetByIdAsync(id);

            if (teacher == null)
            {
                return NotFound();
            }

            return teacher;
        }

        // PUT: api/Lessons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLesson(Guid id, TeacherOutDTO teacher)
        {
            if (id != teacher.Id)
            {
                return BadRequest();
            }

            await _teacherRepository.UpdateAsync(teacher);

            return NoContent();
        }

        // POST: api/Lessons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TeacherOutDTO>> PostLesson(TeacherDTO teacher)
        {
            Guid id = await _teacherRepository.AddAsync(teacher);

            return CreatedAtAction("GetLesson", new { id = id }, teacher);
        }

        // DELETE: api/Lessons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLesson(Guid id)
        {
            var teacher = await _teacherRepository.GetByIdAsync(id);

            if (teacher == null)
            {
                return NotFound();
            }

            await _teacherRepository.DeleteAsync(id);

            return NoContent();
        }

        private bool TeacherExists(Guid id)
        {
            return _context.Teachers.Any(e => e.Id == id);
        }
    }
}
