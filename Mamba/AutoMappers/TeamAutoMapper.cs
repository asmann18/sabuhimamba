using AutoMapper;
using MambaProject.Models;
using MambaProject.ViewModels.TeamVM;

namespace MambaProject.AutoMappers
{
    public class TeamAutoMapper : Profile
    {
        public TeamAutoMapper()
        {
            CreateMap<Team, TeamCreateVM>().ReverseMap();
            CreateMap<Team, TeamUpdateVM>().ForMember(x=>x.IMageUrl,o=>o.Ignore()).ReverseMap();

        }
    }
}
