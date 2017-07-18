using EventPlayer.Event;
using EventPlayer.Model;
using EventPlayer.Test.Stub.Model;

namespace EventPlayer.Test.Stub.Event
{
    public class StubGatewayEvent : PlayEvent<StubGatewayModel>
    {
        private readonly bool gatewayBoolVal;

        public StubGatewayEvent(Id<StubGatewayModel> id, bool gatewayBoolVal)
            : base(id)
        {
            this.gatewayBoolVal = gatewayBoolVal;
        }

        protected override void ApplyChangesTo(StubGatewayModel model)
        {       
        }

        protected virtual void ApplyGatewayChangesTo(StubGatewayModel model)
        {
            model.GatewayBoolVal = this.gatewayBoolVal;
        }
    }
}