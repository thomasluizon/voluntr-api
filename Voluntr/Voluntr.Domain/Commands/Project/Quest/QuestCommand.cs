﻿using Microsoft.AspNetCore.Http;
using Voluntr.Crosscutting.Domain.Commands;
using Voluntr.Domain.DataTransferObjects;

namespace Voluntr.Domain.Commands
{
    public abstract class QuestCommand : CommandResponse<CommandResponseDto>
    {
        public Guid ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public int Reward { get; set; }
        public int MaxVolunteers { get; set; }
        public bool IsRemote { get; set; }
        public AddressCommand Address { get; set; }
        public IFormFile Image { get; set; }
    }
}
