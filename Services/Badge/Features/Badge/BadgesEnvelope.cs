using System.Collections.Generic;

namespace Badge.Features.Badge
{
    public class BadgesEnvelope
    {
        public IEnumerable<Entities.Badge> Badges { get; }

        public BadgesEnvelope(IEnumerable<Entities.Badge> badges)
        {
            Badges = badges;
        }
    }
}