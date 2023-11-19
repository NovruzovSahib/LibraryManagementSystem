using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class Reader
    {
        [Key]
        public int ReaderId { get; set; }
        public string? ReaderName { get; set; }
        public ICollection<Book>? Books { get; set; }
    }
}
