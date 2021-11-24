using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace LessonLog.Domain
{
    public class Сlassroom
    {
        public Guid Id { get; set; }
        public string Number { get; set; }

        public Guid LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }
}
