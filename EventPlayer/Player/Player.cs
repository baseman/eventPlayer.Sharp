using EventPlayer.Event;

namespace EventPlayer.Player
{
    using Model;

    public class Player<TModel>
        where TModel : Aggregate<TModel>
    {
        public virtual void PlayFor(TModel model, PlayEvent<TModel>[] events)
        {
            foreach (var evt in events)
            {
                evt.ApplyTo(model);
            }
        }

        public void PlayFor(TModel model, PlayEvent<TModel> actualEvt)
        {
            actualEvt.ApplyTo(model);
        }
    }
}