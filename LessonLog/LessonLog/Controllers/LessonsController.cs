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
    public class LessonsController : ControllerBase
    {
        private readonly LessonLogContext _context;
        private readonly LessonRepository _lessonRepository;

        public LessonsController(LessonLogContext context)
        {
            _context = context;
            _lessonRepository = new LessonRepository(_context);
        }

        // GET: api/Lessons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lesson>>> GetLessons()
        {
            return await _lessonRepository.GetAllAsync();
        }

        // GET: api/Lessons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lesson>> GetLesson(Guid id)
        {
            var lesson = await _lessonRepository.GetByIdAsync(id);

            if (lesson == null)
            {
                return NotFound();
            }

            return lesson;
        }

        // PUT: api/Lessons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLesson(Guid id, Lesson lesson)
        {
            if (id != lesson.Id)
            {
                return BadRequest();
            }

            await _lessonRepository.UpdateAsync(lesson);

            return NoContent();
        }

        // POST: api/Lessons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Lesson>> PostLesson(Lesson lesson)
        {
            await _lessonRepository.AddAsync(lesson);

            return CreatedAtAction("GetLesson", new { id = lesson.Id }, lesson);
        }

        // DELETE: api/Lessons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLesson(Guid id)
        {
            var lesson = await _lessonRepository.GetByIdAsync(id);
            if (lesson == null)
            {
                return NotFound();
            }

            await _lessonRepository.DeleteAsync(id);

            return NoContent();
        }

        private bool LessonExists(Guid id)
        {
            return _context.Lessons.Any(e => e.Id == id);
        }
    }
}
