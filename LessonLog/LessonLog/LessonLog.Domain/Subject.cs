using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace LessonLog.Domain
{
    public class Subject
    {
        public Guid Id { get; set; }
        public string SubjectName { get; set; }
        public Guid Identifier { get; set; }

        public List<Lesson> Lessons { get; set; }
    }
}
