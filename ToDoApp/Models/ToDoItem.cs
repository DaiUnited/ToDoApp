namespace ToDoApp.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public string JobToDo { get; set; }
        public string Status { get; set; } // Stages: "Incomplete", "In Progress", "Completed"
        public DateTime StartDate { get; set; }
    }
}
