namespace EventPlayer.Model
{
    using EventPlayer.Design;

    public class Aggregate<TModel> : IAggregate<TModel>
        where TModel : Aggregate<TModel>
    {
        public Aggregate()
        {
            this.Id = new Id<TModel>();
        }

        public Id<TModel> Id { get; set; }

        public string AggregateIdVal
        {
            get { return this.Id.Value; }
            set { this.Id = new Id<TModel>(value); }
        }

        public int LatestVersion { get; set; }
    }
}