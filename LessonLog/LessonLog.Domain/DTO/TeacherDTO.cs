using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace LessonLog.Domain
{
    public class TeacherDTO
    {
        public string Name { get; set; }
        public string Post { get; set; }
        public string AcademicPosition { get; set; }
        public string AcademicTitle { get; set; }

        public Guid LessonId { get; set; }
    }
}