using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TaskManagerASP.Filters
{
    public class LogAttribute : ActionFilterAttribute
    {
        private Stopwatch watch;

        public LogAttribute()
        {
            watch = new Stopwatch();
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            FileStream stream = new FileStream("log.txt", FileMode.Append);
            using (var file = new StreamWriter(stream))
            {
                file.WriteLine($"{context.ActionDescriptor.DisplayName} was called at {DateTime.Now}");
            }
            watch.Reset();
            watch.Start();
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            watch.Stop();
            FileStream stream = new FileStream("log.txt", FileMode.Append);
            using (var file = new StreamWriter(stream))
            {
                file.WriteLine($"{context.ActionDescriptor.DisplayName} was executed at {DateTime.Now}.It took {watch.Elapsed}");
            }
        }
    }
}
