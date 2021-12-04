using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using LessonLog.Domain;

namespace LessonLog.Infrastructure.Repository
{
    public class AttendanceRepository
    {
        private readonly LessonLogContext _context;
        public AttendanceRepository(LessonLogContext context)
        {
            _context = context;
        }
        public async Task<List<Attendance>> GetAllAsync()
        {
            return await _context.Attendences.ToListAsync();
        }
        public async Task<Guid> AddAsync(AttendanceDTO attendanceDTO)
        {
            var attendance = new Attendance()
            {
                Status = attendanceDTO.Status,
                Late = attendanceDTO.Late,
                Leaving = attendanceDTO.Leaving,
                LessonId = attendanceDTO.LessonId
            };
            _context.Attendences.Add(attendance);
            await _context.SaveChangesAsync();
            return attendance.Id;
        }
        public async Task<AttendanceOutDTO> GetByIdAsync(Guid id)
        {
            Attendance attendance = await _context.Attendences.FindAsync(id);
            AttendanceOutDTO dto = new AttendanceOutDTO()
            {
                Id = attendance.Id,
                Status = attendance.Status,
                Late = attendance.Late,
                Leaving = attendance.Leaving,
                LessonId = attendance.LessonId
            };
            return dto;
        }
        public async Task UpdateAsync(AttendanceOutDTO attendance)
        {
            Attendance existAttendence = await _context.Attendences.FindAsync(attendance.Id);
            _context.Entry(existAttendence).CurrentValues.SetValues(attendance);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            Attendance attendance = await _context.Attendences.FindAsync(id);
            if  (attendance == null)
            {
                return false;
            }
            else
            {
                _context.Attendences.Remove(attendance);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}
