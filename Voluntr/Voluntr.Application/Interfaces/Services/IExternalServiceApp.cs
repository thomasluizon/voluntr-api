
using Voluntr.Application.ViewModels;

namespace Voluntr.Application.Interfaces.Services
{
    public interface IExternalServiceApp
    {
        Task<ZipCodeInformationViewModel> GetZipCodeInformation(string zipCode);
    }
}
