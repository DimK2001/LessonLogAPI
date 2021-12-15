using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using LessonLog.Domain;

namespace LessonLog.Infrastructure.Repository
{
    public class StudentRepository
    {
        private readonly LessonLogContext _context;
        public StudentRepository(LessonLogContext context)
        {
            _context = context;
        }
        public async Task<List<Student>> GetAllAsync()
        {
            return await _context.Students.ToListAsync();
        }
        public async Task<Guid> AddAsync(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return student.Id;
        }
        public async Task<Student> GetByIdAsync(Guid id)
        {
            var student = await _context.Students.FindAsync(id);
            return student;
        }
        public async Task UpdateAsync(StudentOutDTO student)
        {
            var existStudent = await _context.Students.FindAsync(student.Id);
            _context.Entry(existStudent).CurrentValues.SetValues(student);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return false;
            }
            else
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}
