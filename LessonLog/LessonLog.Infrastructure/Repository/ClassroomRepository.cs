using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using LessonLog.Domain;

namespace LessonLog.Infrastructure.Repository
{
    public class ClassroomRepository
    {
        private readonly LessonLogContext _context;
        public ClassroomRepository(LessonLogContext context)
        {
            _context = context;
        }
        public async Task<List<Classroom>> GetAllAsync()
        {
            return await _context.Classrooms.ToListAsync();
        }
        public async Task<Guid> AddAsync(ClassroomDTO classroomDTO)
        {
            var classroom = new Classroom()
            {
                Number = classroomDTO.Number,
                LessonId = classroomDTO.LessonId
            };
            _context.Classrooms.Add(classroom);
            await _context.SaveChangesAsync();
            return classroom.Id;
        }
        public async Task<ClassroomOutDTO> GetByIdAsync(Guid id)
        {
            var classroom = await _context.Classrooms.FindAsync(id);
            var dto = new ClassroomOutDTO()
            {
                Id = classroom.Id,
                Number = classroom.Number,
                LessonId = classroom.LessonId
            };
            return dto;
        }
        public async Task UpdateAsync(ClassroomOutDTO classroom)
        {
            var existClassroom = await _context.Classrooms.FindAsync(classroom.Id);
            _context.Entry(existClassroom).CurrentValues.SetValues(classroom);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var classroom = await _context.Classrooms.FindAsync(id);
            if (classroom == null)
            {
                return false;
            }
            else
            {
                _context.Classrooms.Remove(classroom);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}