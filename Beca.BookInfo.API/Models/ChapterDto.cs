using Beca.BookInfo.API.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Beca.BookInfo.API.Models
{
    public class ChapterDto
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Chapter Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// A simply description of the Chapter
        /// </summary>
        public string? Description { get; set; }
    }
}