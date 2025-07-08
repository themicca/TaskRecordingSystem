using System.ComponentModel.DataAnnotations;

namespace TaskRecordingSystem.Models
{
    public class ChecklistItem
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public bool IsCompleted { get; set; } = false;
        public DateTime DueDate { get; set; } = DateTime.Now;

        public int TaskItemId { get; set; }
        public TaskItem TaskItem { get; set; } = null!;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DueDate < TaskItem.ReportedDate)
            {
                yield return new ValidationResult(
                    "Due date cannot be sooner than reported date of the task.",
                    new[] { nameof(DueDate) });
            }

            if (DueDate > TaskItem.DueDate)
            {
                yield return new ValidationResult(
                    "Due date of a step cannot be later than due date of the task.",
                    new[] { nameof(DueDate) });
            }
        }
    }
}
