using AutoMapper;
using System.Linq;
using TranslationManagement.Api.Models;
using TranslationManagement.Api.ViewModels;

namespace TranslationManagement.Api
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TranslationJob, TranslationJobResponseViewModel>()
                .ForMember(dest => dest.Translator, opt => opt.MapFrom(src => src.TranslatorId.HasValue? new LinkedEntity()
                {
                    Id = src.TranslatorId.Value,
                    Name = src.Translator.Name
                } : null));

            CreateMap<TranslationJobRequestViewModel, TranslationJob>();

            CreateMap<Translator, TranslatorResponseViewModel>()
                .ForMember(dest => dest.Jobs, opt => opt.MapFrom(src => src.TranslationJobs.Select(j => new LinkedEntity()
                {
                    Id = src.Id,
                    Name = src.Name
                })));

            CreateMap<TranslatorRequestViewModel, Translator>();
        }
    }
}
