namespace TaskRecordingSystem.Models
{
    public class Attachment
    {
        public int Id { get; set; }
        public string FileName { get; set; } = null!;
        public string? FilePath { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.Now;

        public int TaskItemId { get; set; }
        public TaskItem TaskItem { get; set; } = null!;
    }
}
