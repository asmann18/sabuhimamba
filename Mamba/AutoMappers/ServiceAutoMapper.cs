using AutoMapper;
using MambaProject.Models;
using MambaProject.ViewModels.ServiceVMs;

namespace MambaProject.AutoMappers
{
    public class ServiceAutoMapper:Profile
    {
        public ServiceAutoMapper()
        {
            CreateMap<Service, ServiceCreateVM>().ReverseMap(); 
            CreateMap<Service, ServiceUpdateVM>().ReverseMap();
        }
    }
}
