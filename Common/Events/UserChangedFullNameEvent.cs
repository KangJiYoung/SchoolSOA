using System;

namespace Common.Events
{
    public class UserChangedFullNameEvent : IntegrationEvent
    {
        public Guid Guid { get; }
        public string NewFullName { get; }

        public UserChangedFullNameEvent(Guid guid, string newFullName)
        {
            Guid = guid;
            NewFullName = newFullName;
        }
    }
}