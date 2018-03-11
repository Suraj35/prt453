using AutoMapper;

namespace WebApp.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            // Create Automapper profiles
            Mapper.Initialize(m =>
            {
                m.AddProfile<DomainMappingProfile>();
                m.AddProfile<ViewModelMappingProfile>();
            });
        }
    } 
}


