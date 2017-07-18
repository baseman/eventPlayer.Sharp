using EventPlayer.Command;
using EventPlayer.Event;
using EventPlayer.Model;
using EventPlayer.Test.Stub.Event;
using EventPlayer.Test.Stub.Model;

namespace EventPlayer.Test.Stub.Command
{
    public class StubCommand : PlayCommand<StubModel>
    {
        protected override void Validate(StubModel model)
        {
        }

        protected override PlayEvent<StubModel> GetEvent(Id<StubModel> id)
        {
            return new StubChangeEvent(id, false);
        }
    }
}