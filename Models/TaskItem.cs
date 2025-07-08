using System.ComponentModel.DataAnnotations;

namespace TaskRecordingSystem.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Task title is mandatory!")]
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }

        [Required(ErrorMessage = "Reporter need to be assigned.")]
        public int ReporterId { get; set; }
        public AppUser Reporter { get; set; } = null!;

        [Required(ErrorMessage = "Assignee need to be assigned.")]
        public int AssigneeId { get; set; }
        public AppUser Assignee { get; set; } = null!;

        [Required(ErrorMessage = "Company need to be assigned.")]
        public int CompanyId { get; set; }
        public Company Company { get; set; } = null!;

        public int Priority { get; set; } = 1;
        public string Status { get; set; } = "Open";

        public DateTime ReportedDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ResolvedDate { get; set; }

        public ICollection<ChecklistItem> ChecklistItems { get; set; } = new List<ChecklistItem>();
        public ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();
        public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ReportedDate < new DateTime(2020, 1, 1))
            {
                yield return new ValidationResult(
                    "Reported date cannot be sooner than 1. 1. 2020.",
                    new[] { nameof(ReportedDate) });
            }

            if (DueDate.HasValue && DueDate.Value < ReportedDate)
            {
                yield return new ValidationResult(
                    "Due date cannot be sooner than reported date.",
                    new[] { nameof(DueDate) });
            }
        }
    }
}
