namespace EventPlayer.Event
{
    using EventPlayer.Model;

    public class PlayDeletedEvent<TModel> : PlayEvent<TModel> 
        where TModel : Aggregate<TModel>
    {
        public PlayDeletedEvent(Id<TModel> id) : base(id)
        {
        }

        protected internal override void ApplyChangesTo(TModel model)
        {
        }
    }
}