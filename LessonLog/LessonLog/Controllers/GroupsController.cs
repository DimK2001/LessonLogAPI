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
    public class GroupsController : ControllerBase
    {
        private readonly LessonLogContext _context;
        private readonly GroupRepository _groupRepository;

        public GroupsController(LessonLogContext context)
        {
            _context = context;
            _groupRepository = new GroupRepository(_context);
        }

        // GET: api/Lessons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Group>>> GetLessons()
        {
            return await _groupRepository.GetAllAsync();
        }

        // GET: api/Lessons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GroupOutDTO>> GetLesson(Guid id)
        {
            var group = await _groupRepository.GetByIdAsync(id);

            if (group == null)
            {
                return NotFound();
            }

            return group;
        }

        // PUT: api/Lessons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLesson(Guid id, GroupOutDTO group)
        {
            if (id != group.Id)
            {
                return BadRequest();
            }

            await _groupRepository.UpdateAsync(group);

            return NoContent();
        }

        // POST: api/Lessons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GroupOutDTO>> PostLesson(GroupDTO group)
        {
            Guid id = await _groupRepository.AddAsync(group);

            return CreatedAtAction("GetGroup", new { id = id }, group);
        }

        // DELETE: api/Lessons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLesson(Guid id)
        {
            var group = await _groupRepository.GetByIdAsync(id);

            if (group == null)
            {
                return NotFound();
            }

            await _groupRepository.DeleteAsync(id);

            return NoContent();
        }

        private bool GroupExists(Guid id)
        {
            return _context.Groups.Any(e => e.Id == id);
        }
    }
}
