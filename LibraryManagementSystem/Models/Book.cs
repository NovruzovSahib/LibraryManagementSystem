using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }
        public int ReaderId { get; set; }
        public Category? Category { get; set; }
        public Author? Author { get; set; }
        public Reader? Reader { get; set; }
        public string? CategoryName { get; set; }
        public string? AuthorName { get; set; }
        public string? ReaderName { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Language { get; set; }
        public int PageCount { get; set; }
        public DateTime PublicationDate { get; set; }
        public string? ImagePath { get; set; }

    }
}
