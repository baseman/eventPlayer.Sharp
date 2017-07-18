using EventPlayer.Model;

namespace EventPlayer.Test.Stub.Model
{
    public class StubModel : Aggregate<StubModel>
    {
        public bool BoolVal { get; set; }
    }
}