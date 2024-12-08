
using Voluntr.Application.ViewModels;

namespace Voluntr.Application.Interfaces.Services
{
    public interface IExternalServiceApp
    {
        Task<List<string>> GetCities(string uf);
        Task<List<string>> GetUfs();
        Task<ZipCodeInformationViewModel> GetZipCodeInformation(string zipCode);
    }
}
