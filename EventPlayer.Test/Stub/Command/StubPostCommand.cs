using System;
using EventPlayer.Command;
using EventPlayer.Event;
using EventPlayer.Model;
using EventPlayer.Player;
using EventPlayer.Test.Stub.Event;
using EventPlayer.Test.Stub.Model;

namespace EventPlayer.Test.Stub.Command
{
    public class StubPostCommand : PlayCommand<StubModel>
    {
        public StubPostCommand(Player<StubModel> player, bool boolVal)
            : base(player)
        {
            this.BoolVal = boolVal;
        }

        public StubPostCommand(bool boolVal)
            : this(null, boolVal)
        {
            this.BoolVal = boolVal;
        }

        public StubPostCommand()
        {
        }

        public bool BoolVal { get; set; }

        protected override void Validate(StubModel model)
        {
            if (this.BoolVal == model.BoolVal)
            {
                throw new Exception("Why make an edit when it's the same value? Validate failed.");
            }
        }

        protected override PlayEvent<StubModel> GetEvent(Id<StubModel> id)
        {
            return new StubChangeEvent(id, this.BoolVal);
        }
    }
}