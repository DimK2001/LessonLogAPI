using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace LessonLog.Domain
{
    public class Student
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
#nullable enable
        public Guid? IdManagementSys { get; set; }
        public Uri? PhotoURL { get; set; }
        public string? AttestationMark { get; set; }
        public string? ExamMark { get; set; }
#nullable disable
        public bool GroupFlag { get; set; }

        //Внешние ключи
        public Guid GroupId { get; set; }
        public Group Group { get; set; }
        public List<Attendance> Attendances { get; set; }
    }
}
