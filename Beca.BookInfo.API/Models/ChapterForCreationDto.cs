using System.ComponentModel.DataAnnotations;

namespace Beca.BookInfo.API.Models
{
    public class ChapterForCreationDto
    {
        /// <summary>
        /// The Name of the new Chapter
        /// </summary>
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// This is not required. Include a description of this chapter.
        /// </summary>
        [MaxLength(300)]
        public string? Description { get; set; }
    }
}