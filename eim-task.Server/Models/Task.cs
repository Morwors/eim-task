using System.ComponentModel.DataAnnotations;

namespace eim_task.Server.Models
{
    public class JSTask
    {
        public JSTask()
        {
            CreatedAt = DateTime.UtcNow;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        
        public DateTime? CreatedAt { get; set; }

    }
}