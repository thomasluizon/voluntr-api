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
        IUserAchievementRepository userAchievementRepository
    ) : MediatorQueryHandler<GetCauseAchievementsPageQuery, CauseAchievementsPageDto>(mediator)
    {
        public override async Task<CauseAchievementsPageDto> AfterValidation(GetCauseAchievementsPageQuery request)
        {
            if (claimsService.GetCurrentUserType() != UserTypeEnum.Volunteer.GetDescription())
            {
                NotifyError("O usuário informado não é um voluntário");
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

            var userId = claimsService.GetCurrentUserId();

            var completedAchievements = await userAchievementRepository.ListByExpressionAsync(
                y => y.UserId == userId
            );

            var completedAchievementIds = completedAchievements?.Select(y => y.AchievementId).ToList();

            response.Achievements = cause.Achievements.Select(x => new AchievementForCauseAchievementsPageDto
            {
                Id = x.Id,
                Name = x.Name,
                ImageUrl = x.ImageUrl,
                Done = completedAchievementIds?.Contains(x.Id) ?? false,
                Description = $"Complete {x.TaskCount} missões ligadas a(o) {cause.Name.ToLowerInvariant()}."
            }).ToList();

            // TODO: Calculate after implementing tasks
            response.NumberOfQuestsToNextAchievement = 0;

            return response;
        }
    }
}
