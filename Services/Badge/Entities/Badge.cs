using System;

namespace Badge.Entities
{
    public class Badge
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        
        public BadgeType Type { get; set; }
        public bool IsAwarded { get; set; }

        public int AwardThreshold { get; set; }
        public int CurrentThreshold { get; set; }

        public Badge(Guid userId, BadgeType type)
        {
            UserId = userId;
            Type = type;

            AwardThreshold = type == BadgeType.Beginner ? 1 : 5;
        }

        protected Badge()
        {
        }
    }

    public enum BadgeType
    {
        Beginner,
        Expert
    }
}