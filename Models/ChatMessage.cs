namespace TaskRecordingSystem.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string MessageText { get; set; } = null!;
        public DateTime SentAt { get; set; } = DateTime.Now;

        public int TaskItemId { get; set; }
        public TaskItem TaskItem { get; set; } = null!;

        public int AuthorId { get; set; }
        public AppUser Author { get; set; } = null!;
    }
}
