namespace Voluntr.Domain.DataTransferObjects
{
    public class OnboardingTaskDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Redirect { get; set; }
        public bool Done { get; set; }
    }
}
