using Voluntr.Crosscutting.Domain.Helpers.Extensions;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Crosscutting.Domain.Queries.Handlers;
using Voluntr.Domain.DataTransferObjects;
using Voluntr.Domain.Enumerators;
using Voluntr.Domain.Interfaces.Repositories;
using Voluntr.Domain.Interfaces.Services;
using Voluntr.Domain.Queries.Achievement;

namespace Voluntr.Domain.QueryHandlers
{
    public class GetCauseAchievementsPageQueryHandler(
        IMediatorHandler mediator,
        ICauseRepository causeRepository,
        IClaimsService claimsService,
        IUserAchievementRepository userAchievementRepository,
        IVolunteerRepository volunteerRepository,
        IQuestAssignmenttRepository questAssignmenttRepository,
        IUserCauseRepository userCauseRepository
    ) : MediatorQueryHandler<GetCauseAchievementsPageQuery, CauseAchievementsPageDto>(mediator)
    {
        public override async Task<CauseAchievementsPageDto> AfterValidation(GetCauseAchievementsPageQuery request)
        {
            var userId = claimsService.GetCurrentUserId();

            var volunteer = await volunteerRepository.GetFirstByExpressionAsync(x => x.UserId == userId);

            if (volunteer == null)
            {
                NotifyError("O voluntário informado não foi encontrado");
                return null;
            }

            var response = new CauseAchievementsPageDto();

            var cause = await causeRepository.GetFirstByExpressionAsync(
                x => x.Id == request.CauseId,
                x => x.Achievements
            );

            if (cause == null)
            {
                NotifyError("A causa informada não foi encontrada");
                return null;
            }

            var completedAchievements = await userAchievementRepository.ListByExpressionAsync(
                y => y.UserId == userId
            ) ?? [];

            var completedAchievementIds = completedAchievements.Select(y => y.AchievementId).ToList();

            response.Achievements = cause.Achievements
                .OrderBy(x => x.QuestCount)
                .Select(x => new AchievementForCauseAchievementsPageDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImageUrl = x.ImageUrl,
                    Done = completedAchievementIds.Contains(x.Id),
                    Description = $"Complete {x.QuestCount} missões ligadas a(o) {cause.Name.ToLowerInvariant()}."
                }).ToList();

            var relevantUsers = await userCauseRepository.ListByExpressionAsync(
                x => x.CauseId == cause.Id
            ) ?? [];

            var relevantUserIds = relevantUsers.Select(x => x.UserId).ToList();

            var completedQuests = await questAssignmenttRepository.ListByExpressionAsync(
                x => x.VolunteerId == volunteer.Id &&
                     relevantUserIds.Contains(x.Quest.Project.Ngo.UserId) &&
                     x.Status == QuestAssignmentStatusEnum.Approved.GetDescription(),
                x => x.Quest.Project.Ngo
            ) ?? [];

            var totalCompletedQuests = completedQuests.Count;

            var nextAchievement = cause.Achievements
                .OrderBy(a => a.QuestCount)
                .FirstOrDefault(a => a.QuestCount > totalCompletedQuests);

            response.NumberOfQuestsToNextAchievement = nextAchievement != null
                ? nextAchievement.QuestCount - totalCompletedQuests
                : 0;

            return response;
        }
    }
}
