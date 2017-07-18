namespace EventPlayer.Event
{
    using EventPlayer.Model;

    public abstract class PlayEvent<TModel>
        where TModel : Aggregate<TModel>
    {
        protected PlayEvent(Id<TModel> id)
        {
            this.Id = id;
        }

        public Id<TModel> Id { get; set; }

        public int Version { get; set; }

        protected internal abstract void ApplyChangesTo(TModel model);

        public virtual void ApplyTo(TModel model)
        {
            this.ApplyChangesTo(model);
            model.LatestVersion = this.Version;
        }
    }
}