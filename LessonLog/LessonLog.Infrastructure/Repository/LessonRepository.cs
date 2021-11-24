using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using LessonLog.Domain;

namespace LessonLog.Infrastructure.Repository
{
    public class LessonRepository
    {
        private readonly LessonLogContext _context;
        public LessonRepository(LessonLogContext context)
        {
            _context = context;
        }
        public async Task<List<Lesson>> GetAllAsync()
        {
            return await _context.Lessons.ToListAsync();
        }
        public async Task AddAsync(Lesson lesson)
        {
            await _context.Lessons.AddAsync(lesson);
            await _context.SaveChangesAsync();
        }
        public async Task<Lesson> GetByIdAsync(Guid id)
        {
            return await _context.Lessons.FindAsync(id);
        }
        public async Task UpdateAsync(Lesson lesson)
        {
            Lesson existLesson = await _context.Lessons.FindAsync(lesson.Id);
            _context.Entry(existLesson).CurrentValues.SetValues(lesson);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            Lesson lesson = await _context.Lessons.FindAsync(id);
            if (lesson == null)
            {
                return false;
            }
            else
            {
                _context.Lessons.Remove(lesson);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}
