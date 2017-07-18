namespace EventPlayer.Design
{
    using EventPlayer.Model;

    public interface IAggregate<TModel> where TModel : IAggregate<TModel>
    {
        Id<TModel> Id { get; }

        string AggregateIdVal { get; set; }
    }
}