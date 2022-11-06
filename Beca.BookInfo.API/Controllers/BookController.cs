using AutoMapper;
using Beca.BookInfo.API.Models;
using Beca.BookInfo.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Beca.BookInfo.API.Controllers
{
    [ApiController]
    [Route("api/books")]

    public class BooksController : ControllerBase
    {
        private readonly IBookInfoRepository _bookInfoRepository;
        private readonly IMapper _mapper;
        const int maxBooksPageSize = 20;


        public BooksController(IBookInfoRepository bookInfoRepository,
    IMapper mapper)
        {
            _bookInfoRepository = bookInfoRepository ??
                throw new ArgumentNullException(nameof(bookInfoRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get all Books with pagination
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookWithoutChaptersDto>>> GetBooks(
            string? name, string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            if (pageSize > maxBooksPageSize)
            {
                pageSize = maxBooksPageSize;
            }

            var (bookEntities, paginationMetadata) = await _bookInfoRepository
                .GetBooksAsync(name, searchQuery, pageNumber, pageSize);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

            return Ok(_mapper.Map<IEnumerable<BookWithoutChaptersDto>>(bookEntities));
        }

        /// <summary>
        /// Get one specific Book.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeChapters"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooks(int id, bool includeChapters = false)
        {
            var book = await _bookInfoRepository.GetBookAsync(id, includeChapters);
            if (book == null)
            {
                return NotFound();
            }

            if (includeChapters)
            {
                return Ok(_mapper.Map<BookDto>(book));
            }

            return Ok(_mapper.Map<BookWithoutChaptersDto>(book));
        }

    }
}