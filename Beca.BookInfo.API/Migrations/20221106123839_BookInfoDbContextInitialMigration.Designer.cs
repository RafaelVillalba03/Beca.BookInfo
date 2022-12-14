// <auto-generated />
using Beca.BookInfo.API.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Beca.BookInfo.API.Migrations
{
    [DbContext(typeof(BookInfoContext))]
    [Migration("20221106123839_BookInfoDbContextInitialMigration")]
    partial class BookInfoDbContextInitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.10");

            modelBuilder.Entity("Beca.BookInfo.API.Entities.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Alicia en el país de las maravillas"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Momentos estelares de la humanidad"
                        });
                });

            modelBuilder.Entity("Beca.BookInfo.API.Entities.Chapter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BookId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasMaxLength(300)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.ToTable("Chapters");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BookId = 1,
                            Description = "Érase una vez...",
                            Name = "El acceso a la madriguera"
                        },
                        new
                        {
                            Id = 2,
                            BookId = 2,
                            Description = "En 1451...",
                            Name = "La conquista de Bizancio"
                        });
                });

            modelBuilder.Entity("Beca.BookInfo.API.Entities.Chapter", b =>
                {
                    b.HasOne("Beca.BookInfo.API.Entities.Book", "Book")
                        .WithMany("Chapters")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");
                });

            modelBuilder.Entity("Beca.BookInfo.API.Entities.Book", b =>
                {
                    b.Navigation("Chapters");
                });
#pragma warning restore 612, 618
        }
    }
}
