using AutoMapper;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.AutoMapper
{
    public class ViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get
            {
                return "ViewModelMappingProfile";
            }
        }

        public ViewModelMappingProfile()
        {
            ConfigureMappings();
        }

        private void ConfigureMappings()
        {
            CreateMap<Users, RegisterViewModel>().ReverseMap();
            CreateMap<Events, EventsViewModels>().ReverseMap();

        }
    }
}
