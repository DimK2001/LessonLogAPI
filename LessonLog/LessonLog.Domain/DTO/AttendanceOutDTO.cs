using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace LessonLog.Domain
{
    public class AttendanceOutDTO
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public DateTime? Late { get; set; }
        public DateTime? Leaving { get; set; }
        public Guid LessonId { get; set; }
    }
}