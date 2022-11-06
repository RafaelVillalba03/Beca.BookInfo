using AutoMapper;

namespace Beca.BookInfo.API.Profiles
{
    public class ChapterProfile : Profile
    {

        public ChapterProfile()
        {
            CreateMap<Entities.Chapter, Models.ChapterDto>();
            CreateMap<Models.ChapterForCreationDto, Entities.Chapter>();
            CreateMap<Models.ChapterForUpdateDto, Entities.Chapter>();
            CreateMap<Entities.Chapter, Models.ChapterForUpdateDto>();
        }

    }
}