using Voluntr.Application.ViewModels;

namespace Voluntr.Application.Interfaces.Services
{
    public interface IVolunteerServiceApp
    {
        Task<List<VolunteerResponseViewModel>> GetVolunteers();
    }
}
