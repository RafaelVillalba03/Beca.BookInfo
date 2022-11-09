using AutoMapper;
using Beca.BookInfo.API.Models;
using Beca.BookInfo.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Beca.BookInfo.API.Controllers
{
    [Route("api/books/{bookId}/chapters")]
    [ApiController]
    public class ChaptersController : ControllerBase
    {
        private readonly ILogger<ChaptersController> _logger;
        private readonly IBookInfoRepository _bookInfoRepository;
        private readonly IMapper _mapper;

        public ChaptersController(ILogger<ChaptersController> logger,
            IBookInfoRepository bookInfoRepository,
            IMapper mapper)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
            _bookInfoRepository = bookInfoRepository ??
                throw new ArgumentNullException(nameof(bookInfoRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChapterDto>>> GetChapters(
            int bookId)
        {

            if (!await _bookInfoRepository.BookExistsAsync(bookId))
            {
                _logger.LogInformation(
                    $"Book with id {bookId} wasn't found when accessing chapters.");
                return NotFound();
            }

            var chaptersForBook = await _bookInfoRepository.GetChaptersForBookAsync(bookId);

            return Ok(_mapper.Map<IEnumerable<ChapterDto>>(chaptersForBook));
        }

        [HttpGet("{chapterid}", Name = "GetChapter")]
        public async Task<ActionResult<ChapterDto>> GetChapter(
            int bookId, int chapterId)
        {
            if (!await _bookInfoRepository.BookExistsAsync(bookId))
            {
                return NotFound();
            }

            var chapter = await _bookInfoRepository
                .GetChapterForBookAsync(bookId, chapterId);


            if (chapter == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ChapterDto>(chapter));
        }

        [HttpPost]
        public async Task<ActionResult<ChapterDto>> CreateChapter(
           int bookId,
           ChapterForCreationDto chapter)
        {
            if (!await _bookInfoRepository.BookExistsAsync(bookId))
            {
                return NotFound();
            }

            var finalChapter = _mapper.Map<Entities.Chapter>(chapter);

            await _bookInfoRepository.AddChapterForBookAsync(
                bookId, finalChapter);

            await _bookInfoRepository.SaveChangesAsync();

            var createdChapterToReturn =
                _mapper.Map<Models.ChapterDto>(finalChapter);

            return CreatedAtRoute("GetChapter",
                 new
                 {
                     bookId = bookId,
                     chapterId = createdChapterToReturn.Id
                 },
                 createdChapterToReturn);
        }

        [HttpPut("{chapterid}")]
        public async Task<ActionResult> UpdateChapter(int bookId, int chapterId,
            ChapterForUpdateDto chapter)
        {
            if (!await _bookInfoRepository.BookExistsAsync(bookId))
            {
                return NotFound();
            }

            var chapterEntity = await _bookInfoRepository
                .GetChapterForBookAsync(bookId, chapterId);
            if (chapterEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(chapter, chapterEntity);

            await _bookInfoRepository.SaveChangesAsync();

            return NoContent();
        }


        [HttpPatch("{chapterid}")]
        public async Task<ActionResult> PartiallyUpdateChapter(
            int bookId, int chapterId,
            JsonPatchDocument<ChapterForUpdateDto> patchDocument)
        {
            if (!await _bookInfoRepository.BookExistsAsync(bookId))
            {
                return NotFound();
            }

            var chapterEntity = await _bookInfoRepository
                .GetChapterForBookAsync(bookId, chapterId);
            if (chapterEntity == null)
            {
                return NotFound();
            }

            var chapterToPatch = _mapper.Map<ChapterForUpdateDto>(
                chapterEntity);

            patchDocument.ApplyTo(chapterToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(chapterToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(chapterToPatch, chapterEntity);
            await _bookInfoRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{chapterid}")]
        public async Task<ActionResult> DeleteChapter(
            int bookId, int chapterId)
        {
            if (!await _bookInfoRepository.BookExistsAsync(bookId))
            {
                return NotFound();
            }

            var chapterEntity = await _bookInfoRepository
                .GetChapterForBookAsync(bookId, chapterId);
            if (chapterEntity == null)
            {
                return NotFound();
            }

            _bookInfoRepository.DeleteChapter(chapterEntity);
            await _bookInfoRepository.SaveChangesAsync();


            return NoContent();
        }

    }
}