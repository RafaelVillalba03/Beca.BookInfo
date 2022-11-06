using Beca.BookInfo.API.Models;

namespace Beca.BookInfo.API
{
    public class BookDataStore
    {
        public List<BookDto> Books { get; set; }
        public static BookDataStore Current { get; } = new BookDataStore();

        public BookDataStore()
        {
            Books = new List<BookDto>()
            {
                new BookDto()
                {
                    Id = 1,
                    Name = "Alicia en el país de las maravillas",
                    Chapters = new List<ChapterDto>()
                    {
                        new ChapterDto()
                        {
                            Id = 1,
                            Name = "El acceso a la madriguera",
                            Description = "Érase una vez..."
                        },
                        new ChapterDto()
                        {
                            Id = 2,
                            Name = "Una nueva aventura comienza",
                            Description = "El pastel tuvo una aceptación inesperada..."
                        }
                    }
                },
                new BookDto()
                {
                    Id = 2,
                    Name = "Momentos estelares de la Humanidad",
                    Chapters = new List<ChapterDto>()
                    {
                        new ChapterDto()
                        {
                            Id = 3,
                            Name = "La conquista de Bizancio",
                            Description = "En 1451..."
                        },
                         new ChapterDto()
                        {
                            Id = 4,
                            Name = "En busca de la inmortalidad",
                            Description = "A la vuelta del descubrimiento de las Américas..."
                        }
                    }
                }
            };

        }
    }
}