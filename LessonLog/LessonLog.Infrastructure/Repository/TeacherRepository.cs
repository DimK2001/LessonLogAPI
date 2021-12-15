using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using LessonLog.Domain;

namespace LessonLog.Infrastructure.Repository
{
    public class TeacherRepository
    {
        private readonly LessonLogContext _context;
        public TeacherRepository(LessonLogContext context)
        {
            _context = context;
        }
        public async Task<List<Teacher>> GetAllAsync()
        {
            return await _context.Teachers.ToListAsync();
        }
        public async Task<Guid> AddAsync(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();
            return teacher.Id;
        }
        public async Task<Teacher> GetByIdAsync(Guid id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            return teacher;
        }
        public async Task UpdateAsync(TeacherOutDTO teacher)
        {
            var existTeacher = await _context.Teachers.FindAsync(teacher.Id);
            _context.Entry(existTeacher).CurrentValues.SetValues(teacher);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return false;
            }
            else
            {
                _context.Teachers.Remove(teacher);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}
