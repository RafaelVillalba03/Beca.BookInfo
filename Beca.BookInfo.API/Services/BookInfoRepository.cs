using Beca.BookInfo.API.Services;
using Beca.BookInfo.API.DbContexts;
using Beca.BookInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookInfo.API.Services
{
    public class BookInfoRepository : IBookInfoRepository
    {
        private readonly BookInfoContext _context;

        public BookInfoRepository(BookInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            return await _context.Books.OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<bool> BookNameMatchesBookId(string? bookName, int bookId)
        {
            return await _context.Books.AnyAsync(c => c.Id == bookId && c.Name == bookName);
        }

        public async Task<(IEnumerable<Book>, PaginationMetadata)> GetBooksAsync(
            string? name, string? searchQuery, int pageNumber, int pageSize)
        {
            // collection to start from
            var collection = _context.Books as IQueryable<Book>;

            if (!string.IsNullOrWhiteSpace(name))
            {
                name = name.Trim();
                collection = collection.Where(c => c.Name == name);
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(a => a.Name.Contains(searchQuery)
                    || (a.Description != null && a.Description.Contains(searchQuery)));
            }

            var totalItemCount = await collection.CountAsync();

            var paginationMetadata = new PaginationMetadata(
                totalItemCount, pageSize, pageNumber);

            var collectionToReturn = await collection.OrderBy(c => c.Name)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationMetadata);
        }



        public async Task<Book?> GetBookAsync(int bookId, bool includeChapters)
        {
            if (includeChapters)
            {
                return await _context.Books.Include(c => c.Chapters)
                    .Where(c => c.Id == bookId).FirstOrDefaultAsync();
            }

            return await _context.Books
                  .Where(c => c.Id == bookId).FirstOrDefaultAsync();
        }

        public async Task<bool> BookExistsAsync(int bookId)
        {
            return await _context.Books.AnyAsync(c => c.Id == bookId);
        }

        public async Task<Chapter?> GetChapterForBookAsync(
            int bookId,
            int chapterId)
        {
            return await _context.Chapters
               .Where(p => p.BookId == bookId && p.Id == chapterId)
               .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Chapter>> GetChaptersForBookAsync(
            int bookId)
        {
            return await _context.Chapters
                           .Where(p => p.BookId == bookId).ToListAsync();
        }

        public async Task AddChapterForBookAsync(int bookId,
            Chapter chapter)
        {
            var book = await GetBookAsync(bookId, false);
            if (book != null)
            {
                book.Chapters.Add(chapter);
            }
        }

        public void DeleteChapter(Chapter chapter)
        {
            _context.Chapters.Remove(chapter);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}