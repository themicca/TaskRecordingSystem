namespace TaskRecordingSystem.Models
{
    public class AppUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string? Email { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; } = null!;

        public ICollection<TaskItem> ReportedTasks { get; set; } = new List<TaskItem>();
        public ICollection<TaskItem> AssignedTasks { get; set; } = new List<TaskItem>();
        public ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();
    }
}
