using Microsoft.AspNetCore.Mvc;
using TaskRecordingSystem.Models;

namespace TaskRecordingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskItemController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TaskItemController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("{taskId}/upload")]
        public async Task<IActionResult> UploadAttachment(int taskId, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            var fileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine("Attachments", fileName);

            Directory.CreateDirectory("Attachments");
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var attachment = new Attachment
            {
                FileName = fileName,
                FilePath = filePath,
                TaskItemId = taskId
            };

            _context.Attachments.Add(attachment);
            await _context.SaveChangesAsync();

            return Ok(attachment);
        }

        [HttpDelete("attachments/{id}")]
        public async Task<IActionResult> DeleteAttachment(int id)
        {
            var attachment = await _context.Attachments.FindAsync(id);
            if (attachment == null)
                return NotFound();

            if (System.IO.File.Exists(attachment.FilePath))
                System.IO.File.Delete(attachment.FilePath);

            _context.Attachments.Remove(attachment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
