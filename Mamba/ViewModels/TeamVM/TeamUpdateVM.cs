namespace MambaProject.ViewModels.TeamVM
{
    public class TeamUpdateVM
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Title { get; set; }
        public string? IMageUrl { get; set; }
        public IFormFile? Image { get; set; }
    }
}
