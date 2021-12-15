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
        public async Task<Guid> AddAsync(Lesson lesson)
        {
            _context.Lessons.Add(lesson);
            await _context.SaveChangesAsync();
            return lesson.Id;
        }
        public async Task<Lesson> GetByIdAsync(Guid id)
        {
            var lesson = await _context.Lessons.FindAsync(id);
            return lesson;
        }
        public async Task UpdateAsync(LessonOutDTO lesson)
        {
            var existLesson = await _context.Lessons.FindAsync(lesson.Id);
            _context.Entry(existLesson).CurrentValues.SetValues(lesson);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var lesson = await _context.Lessons.FindAsync(id);
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
