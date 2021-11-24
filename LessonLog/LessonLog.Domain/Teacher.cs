using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace LessonLog.Domain
{
    public class Teacher
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Post { get; set; }
        public string AcademicPosition { get; set; }
        public string AcademicTitle { get; set; }

        //Внешние ключи
        public Guid LessinId { get; set; }
        public Lesson Lesson { get; set; }
    }
}
