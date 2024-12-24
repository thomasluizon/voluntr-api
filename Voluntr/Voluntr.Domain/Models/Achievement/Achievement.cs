﻿using Voluntr.Crosscutting.Domain.Models;

namespace Voluntr.Domain.Models
{
    public class Achievement : Entity
    {
        public string Name { get; set; }
        public int TaskCount { get; set; }
        public string ImageUrl { get; set; }
        public Guid? CategoryId { get; set; }

        public virtual AchievementCategory Category { get; set; }
    }
}