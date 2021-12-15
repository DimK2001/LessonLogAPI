using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace LessonLog.Domain
{
    public class LessonDTO
    {
        public string Type { get; set; }
        public int? Number { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Theme { get; set; }
        public int? State { get; set; }

        public Subject Subject { get; set; }
        public Classroom Classroom { get; set; }
    }
}