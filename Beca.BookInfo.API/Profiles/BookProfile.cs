using AutoMapper;

namespace Beca.BookInfo.API.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Entities.Book, Models.BookWithoutChaptersDto>();
            CreateMap<Entities.Book, Models.BookDto>();
        }
    }
}