using System;

namespace EventBus.Messages.Events
{
  public  class IntegrationBaseEvent
    {
        public IntegrationBaseEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTimeOffset.Now;
        }

        public IntegrationBaseEvent(Guid id, DateTimeOffset createDate)
        {
            Id = id;
            CreationDate = createDate;
        }

        public Guid Id { get; private set; }

        public DateTimeOffset CreationDate { get; private set; }
    }
}
