namespace EventPlayer.Command
{
    using EventPlayer.Event;
    using EventPlayer.Model;

    public class PlayDeleteCommand<TModel> : PlayCommand<TModel> where TModel : Aggregate<TModel>
    {
        protected internal override void Validate(TModel model)
        {
        }

        protected internal override PlayEvent<TModel> GetEvent(Id<TModel> id)
        {
            return new PlayDeletedEvent<TModel>(id);
        }
    }
}