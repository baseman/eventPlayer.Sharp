namespace EventPlayer.Model
{
    using EventPlayer.Design;

    public struct Id<TModel> where TModel : IAggregate<TModel>
    {
        public string Value;

        public Id(string id)
        {
            this.Value = id;
        }
    }
}