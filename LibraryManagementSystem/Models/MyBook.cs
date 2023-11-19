using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem.Models
{
    public class MyBook
    {
        [Key]
        public int BookId { get; set; }
        public string? Title { get; set; }
        public string? CategoryName { get; set; }
        public string? AuthorName { get; set; }
        public string? Description { get; set; }
        public string? Language { get; set; }
        public int PageCount { get; set; }
        public DateTime PublicationDate { get; set; }
        public string? ImagePath { get; set; }
    }
}
