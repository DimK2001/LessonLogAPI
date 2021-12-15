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
        public async Task<ActionResult<LessonOutDTO>> GetLesson(Guid id)
        {
            var lesson = await _lessonRepository.GetByIdAsync(id);

            if (lesson == null)
            {
                return NotFound();
            }
            var dto = new LessonOutDTO()
            {
                Id = lesson.Id,
                Type = lesson.Type,
                Number = lesson.Number,
                Date = lesson.Date,
                StartTime = lesson.StartTime,
                EndTime = lesson.EndTime,
                Theme = lesson.Theme,
                State = lesson.State,
                Subject = lesson.Subject,
                Classroom = lesson.Classroom
            };
            return dto;
        }

        // PUT: api/Lessons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLesson(Guid id, LessonOutDTO lesson)
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
        public async Task<ActionResult<LessonOutDTO>> PostLesson(LessonDTO lessonDTO)
        {
            var lesson = new Lesson()
            {
                Type = lessonDTO.Type,
                Number = lessonDTO.Number,
                Date = lessonDTO.Date,
                StartTime = lessonDTO.StartTime,
                EndTime = lessonDTO.EndTime,
                Theme = lessonDTO.Theme,
                State = lessonDTO.State,
                Subject = lessonDTO.Subject,
                Classroom = lessonDTO.Classroom
            };
            Guid id = await _lessonRepository.AddAsync(lesson);

            return CreatedAtAction("GetLesson", new { id = id }, lesson);
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
