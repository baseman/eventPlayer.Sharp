using System;
using System.Collections.Generic;

using EventPlayer.Model;

namespace EventPlayer.Event
{
    public class SubscribedPlayEvent<TModel, TEventData> : PlayEvent<TModel>
        where TModel : Aggregate<TModel>
        where TEventData : SubscribedPlayEvent<TModel, TEventData>
    {
        private static readonly List<Func<TEventData, Action<TModel>>> eventSubscriptions = new List<Func<TEventData, Action<TModel>>>();

        public SubscribedPlayEvent(Id<TModel> id)
            : base(id)
        {
        }

        protected internal override void ApplyChangesTo(TModel model)
        {
            foreach (var apply in eventSubscriptions)
            {
                apply((TEventData)this)(model);
            }
        }

        public static void OnPlaybackApply(Func<TEventData, Action<TModel>> changeSubscription)
        {
            eventSubscriptions.Add(changeSubscription);
        }

        public override void ApplyTo(TModel model)
        {
            this.ApplyChangesTo(model);
            model.LatestVersion = this.Version;
        }
    }
}