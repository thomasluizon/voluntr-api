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
    public class GetAchievementsPageQueryHandler(
        IMediatorHandler mediator,
        IAchievementRepository achievementRepository,
        IUserAchievementRepository userAchievementRepository,
        ICauseRepository causeRepository,
        IClaimsService claimsService
    ) : MediatorQueryHandler<GetAchievementsPageQuery, AchievementsPageDto>(mediator)
    {
        public override async Task<AchievementsPageDto> AfterValidation(GetAchievementsPageQuery request)
        {
            if (claimsService.GetCurrentUserType() != UserTypeEnum.Volunteer.GetDescription())
            {
                NotifyError("O usuário informado não é um voluntário");
            }

            var response = new AchievementsPageDto();

            var generalAchievements = await achievementRepository.ListByExpressionAsync(
                x => x.CauseId == null
            );

            response.Achievements = generalAchievements
                .OrderBy(x => x.QuestCount)
                .Select(x => new AchievementForAchievementsPageDto
                {
                    Id = x.Id,
                    ImageUrl = x.ImageUrl,
                    Name = x.Name
                }).ToList();

            var causes = await causeRepository.ListAllAsync(x => x.Achievements);

            var userId = claimsService.GetCurrentUserId();

            var completedAchievements = await userAchievementRepository.ListByExpressionAsync(
                y => y.UserId == userId
            );

            var completedAchievementIds = completedAchievements?.Select(y => y.AchievementId).ToList();

            response.Causes = causes
                .OrderBy(x => x.Name)
                .Select(x =>
                {
                    var totalAchievements = x.Achievements.Count;
                    var completedCount = x.Achievements.Count(a => completedAchievementIds?.Contains(a.Id) ?? false);

                    return new CauseForAchievementsPageDto
                    {
                        Id = x.Id,
                        ImageUrl = x.ImageUrl,
                        Name = x.Name,
                        TotalAchievements = totalAchievements,
                        CompletedAchievements = completedCount
                    };
                }).ToList();

            return response;
        }
    }
}
