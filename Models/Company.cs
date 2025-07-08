namespace TaskRecordingSystem.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public ICollection<AppUser> Users { get; set; } = new List<AppUser>();
        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}
