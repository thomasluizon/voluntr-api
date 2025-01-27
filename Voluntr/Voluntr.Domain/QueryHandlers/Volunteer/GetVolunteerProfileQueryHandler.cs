using Voluntr.Crosscutting.Domain.Helpers.Extensions;
using Voluntr.Crosscutting.Domain.MediatR;
using Voluntr.Crosscutting.Domain.Queries.Handlers;
using Voluntr.Domain.DataTransferObjects;
using Voluntr.Domain.Enumerators;
using Voluntr.Domain.Interfaces.Repositories;
using Voluntr.Domain.Interfaces.Services;
using Voluntr.Domain.Queries;

namespace Voluntr.Domain.QueryHandlers
{
    public class GetVolunteerProfileQueryHandler(
        IMediatorHandler mediator,
        IClaimsService claimsService,
        IVolunteerRepository volunteerRepository,
        IQuestAssignmentRepository questAssignmentRepository,
        IAchievementRepository achievementRepository,
        IUserAchievementRepository userAchievementRepository
    ) : MediatorQueryHandler<GetVolunteerProfileQuery, VolunteerProfileDto>(mediator)
    {
        public override async Task<VolunteerProfileDto> AfterValidation(GetVolunteerProfileQuery request)
        {
            var userId = claimsService.GetCurrentUserId();

            var volunteer = await volunteerRepository.GetFirstByExpressionAsync(
                x => x.UserId == userId,
                x => x.User
            );

            if (volunteer == null)
            {
                NotifyError("O voluntário informado não foi encontrado");
                return null;
            }

            var response = new VolunteerProfileDto
            {
                Volunteer = new VolunteerInformationDto
                {
                    Name = $"{volunteer.User.Name} {volunteer.Surname}",
                    Nickname = volunteer.Nickname,
                    ImageUrl = volunteer.User.Picture,
                    RegisterYear = volunteer.CreatedAt.ToBrazilianTimezone().Year.ToString()
                }
            };

            var questsCompleted = await questAssignmentRepository.ListByExpressionAsync(
                x => x.VolunteerId == volunteer.Id &&
                x.Status == QuestAssignmentStatusEnum.Approved.GetDescription(),
                x => x.Quest
            );

            response.TotalCoins = questsCompleted?.Sum(x => x.Quest.Reward) ?? 0;
            response.QuestsCompleted = questsCompleted?.Count ?? 0;

            var generalAchievements = await achievementRepository.ListByExpressionAsync(x => x.CauseId == null);

            var completedAchievements = await userAchievementRepository.ListByExpressionAsync(
                y => y.UserId == userId
            );

            var completedAchievementIds = completedAchievements?.Select(y => y.AchievementId).ToList();

            response.Achievements = generalAchievements.Select(x => new AchievementForAchievementsPageDto
            {
                Id = x.Id,
                Name = x.Name,
                ImageUrl = x.ImageUrl,
                Done = completedAchievementIds?.Contains(x.Id) ?? false
            }).ToList();

            return response;
        }
    }
}
