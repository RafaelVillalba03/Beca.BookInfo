using Beca.BookInfo.API.Entities;

namespace Beca.BookInfo.API.Services
{
    public interface IBookInfoRepository
    {
        Task<IEnumerable<Book>> GetBooksAsync();
        Task<(IEnumerable<Book>, PaginationMetadata)> GetBooksAsync(
            string? name, string? searchQuery, int pageNumber, int pageSize);
        Task<Book?> GetBookAsync(int BookId, bool includeChapters);
        Task<bool> BookExistsAsync(int BookId);
        Task<IEnumerable<Chapter>> GetChaptersForBookAsync(int BookId);
        Task<Chapter?> GetChapterForBookAsync(int BookId,
            int chapterId);
        Task AddChapterForBookAsync(int BookId, Chapter chapter);
        void DeleteChapter(Chapter chapter);
        Task<bool> BookNameMatchesBookId(string? BookName, int BookId);
        Task<bool> SaveChangesAsync();
    }
}