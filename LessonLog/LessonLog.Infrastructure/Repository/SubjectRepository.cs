using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using LessonLog.Domain;

namespace LessonLog.Infrastructure.Repository
{
    public class SubjectRepository
    {
        private readonly LessonLogContext _context;
        public SubjectRepository(LessonLogContext context)
        {
            _context = context;
        }
        public async Task<List<Subject>> GetAllAsync()
        {
            return await _context.Subjects.ToListAsync();
        }
        public async Task<Guid> AddAsync(SubjectDTO subjectDTO)
        {
            var subject = new Subject()
            {
                SubjectName = subjectDTO.SubjectName,
                Identifier = subjectDTO.Identifier
            };
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();
            return subject.Id;
        }
        public async Task<SubjectOutDTO> GetByIdAsync(Guid id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            var dto = new SubjectOutDTO()
            {
                SubjectName = subject.SubjectName,
                Identifier = subject.Identifier
            };
            return dto;
        }
        public async Task UpdateAsync(SubjectOutDTO subject)
        {
            var existSubject = await _context.Subjects.FindAsync(subject.Id);
            _context.Entry(existSubject).CurrentValues.SetValues(subject);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
            {
                return false;
            }
            else
            {
                _context.Subjects.Remove(subject);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}
