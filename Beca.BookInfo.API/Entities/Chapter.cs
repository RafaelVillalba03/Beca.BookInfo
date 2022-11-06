using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Beca.BookInfo.API.Entities
{
    public class Chapter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(300)]
        public string? Description { get; set; }

        [ForeignKey("BookId")]
        public Book? Book { get; set; }
        public int BookId { get; set; }

        public Chapter(string name)
        {
            Name = name;
        }
    }
}