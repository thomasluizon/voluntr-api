using Microsoft.AspNetCore.Http;

namespace Voluntr.Application.ViewModels
{
    public class UploadPictureRequestViewModel
    {
        /// <summary>
        /// Foto do usuário
        /// </summary>
        public IFormFile Picture { get; set; }
    }
}
