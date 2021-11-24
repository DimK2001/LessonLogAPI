using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace LessonLog.Domain
{
    public class Group
    {
        public Guid Id { get; set; }
        public string GroupNumber { get; set; }
        public string DirectionNumber { get; set; }
        public string DirectionName { get; set; }

        //Внешние ключи
        public Guid? LessonId { get; set; }
        public Lesson Lesson { get; set; }
        public List<Student> Students { get; set; }
    }
}
