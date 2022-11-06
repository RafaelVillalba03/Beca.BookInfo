using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Beca.BookInfo.API.Models
{
    public class BookDto
    {
        /// <summary>
        /// The Id is required to identify the Book
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The name of the Book
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Collection of Chapters associated with a Book
        /// </summary>
        public ICollection<ChapterDto> Chapters { get; set; } = new List<ChapterDto>();
    }
}

