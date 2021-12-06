using System;
using Xunit;
using LessonLog;

namespace TestProject1
{
    public class CrudTest
    {
        [Fact]
        public void AddTest()
        {
            var testHelper = new TestHelper();
            var context = testHelper.Context;

            context.SaveChanges();
        }
    }
}
