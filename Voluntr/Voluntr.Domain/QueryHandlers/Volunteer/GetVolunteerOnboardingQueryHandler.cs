using AutoMapper;
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
    public class GetVolunteerOnboardingQueryHandler(
        IMediatorHandler mediator,
        IClaimsService claimsService,
        IOnboardingTaskRepository onboardingTaskRepository,
        IVolunteerRepository volunteerRepository,
        IUserCauseRepository userCauseRepository,
        IMapper mapper
    ) : MediatorQueryHandler<GetVolunteerOnboardingQuery, List<OnboardingTaskDto>>(mediator)
    {
        public override async Task<List<OnboardingTaskDto>> AfterValidation(GetVolunteerOnboardingQuery request)
        {
            if (claimsService.GetCurrentUserType() != UserTypeEnum.Volunteer.GetDescription())
            {
                NotifyError("O usuário informado não é um voluntário");
                return null;
            }

            var volunteer = await volunteerRepository.GetFirstByExpressionAsync(
                x => x.UserId == claimsService.GetCurrentUserId(),
                x => x.User
            );

            if (volunteer == null) 
            {
                NotifyError("O voluntário informado não foi encontrado");
                return null;
            }

            var response = new List<OnboardingTaskDto>();

            var tasks = await onboardingTaskRepository.ListAllAsync();

            foreach (var task in tasks) 
            {
                if (task.Type == OnboardingTaskEnum.Picture.GetDescription())
                {
                    var taskDto = mapper.Map<OnboardingTaskDto>(task);

                    taskDto.Done = !string.IsNullOrEmpty(volunteer.User.Picture);

                    response.Add(taskDto);
                }
                else if (task.Type == OnboardingTaskEnum.Cause.GetDescription())
                {
                    var taskDto = mapper.Map<OnboardingTaskDto>(task);

                    taskDto.Done = await userCauseRepository.ExistsByExpressionAsync(
                        x => x.UserId == volunteer.UserId
                    );

                    response.Add(taskDto);
                }
            }

            return response;
        }
    }
}
