using System;

namespace TaskManagerASP.Models
{
    public class TaskShortIndexViewModel
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public int RequiredHours { get; set; }
    }
    public class TaskIndexViewModel : TaskShortIndexViewModel
    {
        public int ExecutitiveId { get; set; }
        public int CreatorId { get; set; }
        public string ExecutitiveName { get; set; }
        public string CreatorName { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastEditedOn { get; set; }
        public bool IsCompleted { get; set; }
    }
}
