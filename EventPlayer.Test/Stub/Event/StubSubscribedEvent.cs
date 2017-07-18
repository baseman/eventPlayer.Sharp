using EventPlayer.Event;
using EventPlayer.Model;
using EventPlayer.Test.Stub.Model;

namespace EventPlayer.Test.Stub.Event
{
    public class StubSubscribedEvent : SubscribedPlayEvent<StubModel, StubSubscribedEvent>
    {
        public readonly bool boolVal;

        public StubSubscribedEvent(Id<StubModel> id, bool boolVal)
            : base(id)
        {
            this.boolVal = boolVal;
        }
        
    }
}