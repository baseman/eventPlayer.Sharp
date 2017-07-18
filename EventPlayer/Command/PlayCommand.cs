namespace EventPlayer.Command
{
    using EventPlayer.Event;
    using EventPlayer.Model;
    using EventPlayer.Player;

    public abstract class PlayCommand<TModel> 
        where TModel : Aggregate<TModel>
    {
        private readonly Player<TModel> player;

        protected PlayCommand(Player<TModel> player)
        {
            this.player = player;
        }

        protected PlayCommand()
        {
        }

        protected internal abstract void Validate(TModel model);

        protected internal abstract PlayEvent<TModel> GetEvent(Id<TModel> id);

        public PlayEvent<TModel> ExecuteOn(TModel model)
        {
            this.Validate(model);
            var evt = this.GetEvent(model.Id);
            evt.Version = model.LatestVersion + 1;

            if (this.player != null)
            {
                evt.ApplyTo(model);
            }
            
            return evt;
        }
    }
}