using System.ComponentModel.DataAnnotations;

namespace Beca.BookInfo.API.Models
{
    public class ChapterForUpdateDto
    {
        /// <summary>
        /// This is the name of the chapter
        /// </summary>
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// This is not required. Update the description of this chapter.
        /// </summary>
        [MaxLength(300)]
        public string? Description { get; set; }

    }
}