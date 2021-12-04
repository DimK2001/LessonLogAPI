using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace LessonLog.Domain
{
    public class GroupOutDTO
    {
        public Guid Id { get; set; }
        public string GroupNumber { get; set; }
        public string DirectionNumber { get; set; }
        public string DirectionName { get; set; }

        public Guid LessonId { get; set; }
    }
}
