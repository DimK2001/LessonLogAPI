using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using LessonLog.Domain;

namespace LessonLog.Infrastructure.Repository
{
    public class GroupRepository
    {
        private readonly LessonLogContext _context;
        public GroupRepository(LessonLogContext context)
        {
            _context = context;
        }
        public async Task<List<Group>> GetAllAsync()
        {
            return await _context.Groups.ToListAsync();
        }
        public async Task<Guid> AddAsync(Group group)
        {
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();
            return group.Id;
        }
        public async Task<Group> GetByIdAsync(Guid id)
        {
            Group group = await _context.Groups.FindAsync(id);
            return group;
        }
        public async Task UpdateAsync(GroupOutDTO group)
        {
            Group existGroup = await _context.Groups.FindAsync(group.Id);
            _context.Entry(existGroup).CurrentValues.SetValues(group);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            Group group = await _context.Groups.FindAsync(id);
            if (group == null)
            {
                return false;
            }
            else
            {
                _context.Groups.Remove(group);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}

