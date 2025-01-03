﻿
using Voluntr.Application.ViewModels;

namespace Voluntr.Application.Interfaces.Services
{
    public interface ICauseServiceApp
    {
        Task<AchievementsPageViewModel> GetAchievementsPage();
        Task<CauseAchievementsPageViewModel> GetCauseAchievementsPage(string id);
    }
}
