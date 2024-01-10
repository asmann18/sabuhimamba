using MambaProject.Models.Common;

namespace MambaProject.Models
{
    public class Team:BaseEntity
    {
        public string FullName { get; set; }
        public string Title { get; set; }
        public string IMageUrl { get; set; }
    }
}
