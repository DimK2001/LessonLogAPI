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
    public class SubjectsController : ControllerBase
    {
        private readonly LessonLogContext _context;
        private readonly SubjectRepository _subjectRepository;

        public SubjectsController(LessonLogContext context)
        {
            _context = context;
            _subjectRepository = new SubjectRepository(_context);
        }

        // GET: api/Lessons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subject>>> GetLessons()
        {
            return await _subjectRepository.GetAllAsync();
        }

        // GET: api/Lessons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubjectOutDTO>> GetLesson(Guid id)
        {
            var subject = await _subjectRepository.GetByIdAsync(id);

            if (subject == null)
            {
                return NotFound();
            }

            return subject;
        }

        // PUT: api/Lessons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLesson(Guid id, SubjectOutDTO subject)
        {
            if (id != subject.Id)
            {
                return BadRequest();
            }

            await _subjectRepository.UpdateAsync(subject);

            return NoContent();
        }

        // POST: api/Lessons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SubjectOutDTO>> PostLesson(SubjectDTO subject)
        {
            Guid id = await _subjectRepository.AddAsync(subject);

            return CreatedAtAction("GetSubject", new { id = id }, subject);
        }

        // DELETE: api/Lessons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLesson(Guid id)
        {
            var subject = await _subjectRepository.GetByIdAsync(id);

            if (subject == null)
            {
                return NotFound();
            }

            await _subjectRepository.DeleteAsync(id);

            return NoContent();
        }

        private bool SubjectExists(Guid id)
        {
            return _context.Subjects.Any(e => e.Id == id);
        }
    }
}
