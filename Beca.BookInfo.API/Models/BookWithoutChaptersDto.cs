namespace Beca.BookInfo.API.Models
{
    public class BookWithoutChaptersDto
    {
        /// <summary>
        /// The id of the book
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the book
        /// </summary>
        public string Name { get; set; } = string.Empty;

    }
}