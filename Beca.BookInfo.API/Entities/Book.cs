using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beca.BookInfo.API.Entities
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(300)]
        public string? Description { get; set; }

        public ICollection<Chapter> Chapters { get; set; }
               = new List<Chapter>();

        public Book(string name)
        {
            Name = name;
        }
    }
}