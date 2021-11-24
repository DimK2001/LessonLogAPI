using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace LessonLog.Domain
{
    public class Attendance
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public DateTime Late { get; set; }
        public DateTime Leaving { get; set; }
        public DateTime PresenceTime { get; set; }
        public float PresencePercentage { get; set; }

        //Внешние ключи
        public Guid LessonId { get; set; }
        public Lesson Lesson { get; set; }
        public Guid StudentId { get; set; }
        public Student Student { get; set; }
    }
}
