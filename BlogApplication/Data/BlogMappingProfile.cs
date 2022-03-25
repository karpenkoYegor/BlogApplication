using AutoMapper;
using BlogApplication.Data.Entities;
using BlogApplication.Models;

namespace BlogApplication.Data
{
    public class BlogMappingProfile : Profile
    {
        public BlogMappingProfile()
        {
            CreateMap<ArticleViewModel, Article>()
                .ForMember(a => a.HeroImage, ex => ex.MapFrom(i => i.HeroImagePath))
                .ForMember(a => a.Id, ex => ex.MapFrom(i => i.Id))
                .ForMember(a => a.Name, ex => ex.MapFrom(i => i.Name))
                .ForMember(a => a.ShortDescription, ex => ex.MapFrom(i => i.ShortDescription))
                .ForMember(a => a.Description, ex => ex.MapFrom(i => i.Description))
                .ForMember(a => a.CategoryId, ex => ex.MapFrom(i => i.CategoryId))
                .ReverseMap();
            CreateMap<CategoryViewModel, Category>()
                .ReverseMap();
            CreateMap<TagViewModel, Tag>()
                .ForMember(t => t.Name, ex => ex.MapFrom(i => i.TagName))
                .ReverseMap();
        }
    }
}
