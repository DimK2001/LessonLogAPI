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

        // GET: api/Teachers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetTeachers()
        {
            return await _teacherRepository.GetAllAsync();
        }

        // GET: api/Teachers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherOutDTO>> GetTeacher(Guid id)
        {
            var teacher = await _teacherRepository.GetByIdAsync(id);

            if (teacher == null)
            {
                return NotFound();
            }
            var dto = new TeacherOutDTO()
            {
                Name = teacher.Name,
                Post = teacher.Post,
                AcademicPosition = teacher.AcademicPosition,
                AcademicTitle = teacher.AcademicTitle,
                LessonId = teacher.LessonId
            };
            return dto;
        }

        // PUT: api/Teachers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeacher(Guid id, TeacherOutDTO teacher)
        {
            if (id != teacher.Id)
            {
                return BadRequest();
            }

            await _teacherRepository.UpdateAsync(teacher);

            return NoContent();
        }

        // POST: api/Teachers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TeacherOutDTO>> PostTeacher(TeacherDTO teacherDTO)
        {
            var teacher = new Teacher()
            {
                Name = teacherDTO.Name,
                Post = teacherDTO.Post,
                AcademicPosition = teacherDTO.AcademicPosition,
                AcademicTitle = teacherDTO.AcademicTitle,
                LessonId = teacherDTO.LessonId
            };
            Guid id = await _teacherRepository.AddAsync(teacher);

            return CreatedAtAction("GetLesson", new { id = id }, teacher);
        }

        // DELETE: api/Teachers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(Guid id)
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
