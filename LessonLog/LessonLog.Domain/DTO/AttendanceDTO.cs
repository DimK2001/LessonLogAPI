using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace LessonLog.Domain
{
    public class AttendanceDTO
    {
        public string Status { get; set; }
        public DateTime Comming { get; set; }
        public DateTime Leaving { get; set; }
        public Guid LessonId { get; set; }
    }
}