using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class Worker
    {
        [Key]
        public int WorkerId { get; set; }
        public string? WorkerName { get; set; }
    }
}
