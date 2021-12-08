using System;
using Xunit;
using LessonLog;
using LessonLog.Domain;
using System.Linq;

namespace TestProject1
{
    public class CrudTest
    {
        [Fact]
        public void AddTest()
        {
            var testHelper = new TestHelper();
            var _context = testHelper.Context;

            var lesson = new Lesson
            {
                Id = new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E4"),
                Type = "testType",
                Number = 1,
                Theme = "testTheme"
            };
            lesson.Subject = new Subject();
            lesson.Classroom = new Classroom();
            lesson.Subject.SubjectName = "Name";
            lesson.Classroom.Number = "301/1";
            _context.Lessons.Add(lesson);

            var Lesson = _context.Lessons.Find(new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E4"));
            Assert.Equal("testTheme", Lesson.Theme);
            Assert.Equal("301/1", Lesson.Classroom.Number);
            Assert.Equal("Name", Lesson.Subject.SubjectName);

            _context.SaveChanges();
        }
        [Fact]
        public void FindTest()
        {
            var testHelper = new TestHelper();
            var _context = testHelper.Context;

            var lesson1 = new Lesson
            {
                Id = new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E5"),
                Type = "testType",
                Number = 1,
                Theme = "testTheme"
            };
            var lesson2 = new Lesson
            {
                Id = new Guid("E9168C5E-CEB2-4faa-B6BF-329BF39FA1E5"),
                Type = "testType2",
                Number = 2,
                Theme = "testTheme2"
            };
            lesson1.Classroom = new Classroom();
            lesson2.Classroom = new Classroom();
            lesson1.Classroom.Number = "500/1";
            lesson2.Classroom.Number = "301/1";

            _context.Lessons.Add(lesson1);
            _context.Lessons.Add(lesson2);
            _context.SaveChanges();


            var l1 = _context.Lessons.Where(r => r.Classroom.Number == "500/1").FirstOrDefault();
            var l2 = _context.Lessons.Where(r => r.Classroom.Number == "301/1").FirstOrDefault();

            Assert.Equal(new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E5"), l1.Id);
            Assert.Equal(new Guid("E9168C5E-CEB2-4faa-B6BF-329BF39FA1E5"), l2.Id);
        }
    }
}
