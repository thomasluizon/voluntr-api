﻿using Voluntr.Crosscutting.Domain.Models;

namespace Voluntr.Domain.Models
{
    public class Volunteer : Entity
    {
        public Guid UserId { get; set; }

        public virtual User User { get; set; }
    }
}
