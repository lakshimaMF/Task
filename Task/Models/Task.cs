namespace Task.Models
{
    public class Task
    {
        public int TaskID { get; set; }
        public int UserID { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public string PriorityLevel { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime DueDate { get; set; }
        public int StatusID { get; set; }
    }
}
