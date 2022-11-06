using Beca.BookInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Beca.BookInfo.API.DbContexts
{
    public class BookInfoContext : DbContext
    {
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Chapter> Chapters { get; set; } = null!;

        public BookInfoContext(DbContextOptions<BookInfoContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasData(
               new Book("Alicia en el país de las maravillas")
               {
                   Id = 1,
                   Description = "Cuento infantil."
               },
               new Book("Momentos estelares de la humanidad")
               {
                   Id = 2,
                   Description = "Probablemente una de las mejores obras de Stefan Zwaig"
               });
               
            modelBuilder.Entity<Chapter>()
             .HasData(
               new Chapter("El acceso a la madriguera")
               {
                   Id = 1,
                   BookId = 1,
                   Description = "Érase una vez..."
               },
               new Chapter("Una nueva aventura comienza")
               {
                   Id = 2,
                   BookId = 1,
                   Description = "El pastel tuvo una aceptación inesperada..."
               },
               new Chapter("La conquista de Bizancio")
               {
                   Id = 3,
                   BookId = 2,
                   Description = "En 1451..."
               },
               new Chapter("En busca de la inmortalidad")
               {
                   Id = 4,
                   BookId = 2,
                   Description = "A la vuelta del descubrimiento de las Américas..."
               }
               );
            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("connectionstring");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}