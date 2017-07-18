using EventPlayer.Event;
using EventPlayer.Model;
using EventPlayer.Test.Stub.Model;

namespace EventPlayer.Test.Stub.Event
{
    public class StubChangeEvent : PlayEvent<StubModel>
    {
        public StubChangeEvent(Id<StubModel> id, bool boolVal) : base(id)
        {
            this.BoolVal = boolVal;
        }

        public bool BoolVal { get; set; }

        protected override void ApplyChangesTo(StubModel model)
        {
            model.BoolVal = this.BoolVal;
        }
    }
}