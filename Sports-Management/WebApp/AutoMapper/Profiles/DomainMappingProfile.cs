using AutoMapper;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.AutoMapper
{
    public class DomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get
            {
                return "DomainMappingProfile";
            }
        }

        public DomainMappingProfile()
        {
            ConfigureMappings();
        }

        private void ConfigureMappings()
        {
            CreateMap<RegisterViewModel, Users>().ReverseMap();
            CreateMap<EventsViewModels, Events>().ReverseMap();

        }
    }
}
