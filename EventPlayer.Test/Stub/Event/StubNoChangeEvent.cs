using EventPlayer.Event;
using EventPlayer.Model;
using EventPlayer.Test.Stub.Model;

namespace EventPlayer.Test.Stub.Event
{
    public class StubNoChangeEvent : PlayEvent<StubModel>
    {
        public StubNoChangeEvent(Id<StubModel> id) : base(id)
        {
        }

        protected override void ApplyChangesTo(StubModel model)
        {
        }
    }
}