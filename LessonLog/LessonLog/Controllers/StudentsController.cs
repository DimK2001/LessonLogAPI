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
    public class StudentsController : ControllerBase
    {
        private readonly LessonLogContext _context;
        private readonly StudentRepository _studentRepository;

        public StudentsController(LessonLogContext context)
        {
            _context = context;
            _studentRepository = new StudentRepository(_context);
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _studentRepository.GetAllAsync();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentOutDTO>> GetStudent(Guid id)
        {
            var student = await _studentRepository.GetByIdAsync(id);

            if (student == null)
            {
                return NotFound();
            }
            var dto = new StudentOutDTO()
            {
                Name = student.Name,
                IdManagementSys = student.IdManagementSys,
                PhotoURL = student.PhotoURL,
                AttestationMark = student.AttestationMark,
                ExamMark = student.ExamMark,
                GroupFlag = student.GroupFlag,
                GroupId = student.GroupId
            };
            return dto;
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(Guid id, StudentOutDTO student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            await _studentRepository.UpdateAsync(student);

            return NoContent();
        }

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StudentOutDTO>> PostStudent(StudentDTO studentDTO)
        {
            var student = new Student()
            {
                Name = studentDTO.Name,
                IdManagementSys = studentDTO.IdManagementSys,
                PhotoURL = studentDTO.PhotoURL,
                AttestationMark = studentDTO.AttestationMark,
                ExamMark = studentDTO.ExamMark,
                GroupFlag = studentDTO.GroupFlag,
                GroupId = studentDTO.GroupId
            };
            Guid id = await _studentRepository.AddAsync(student);

            return CreatedAtAction("GetStudent", new { id = id }, student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            var student = await _studentRepository.GetByIdAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            await _studentRepository.DeleteAsync(id);

            return NoContent();
        }

        private bool StudentExists(Guid id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
