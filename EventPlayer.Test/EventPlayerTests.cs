using System;
using EventPlayer.Command;
using EventPlayer.Event;
using EventPlayer.Model;
using EventPlayer.Player;
using EventPlayer.Test.Stub.Command;
using EventPlayer.Test.Stub.Event;
using EventPlayer.Test.Stub.Model;
using Xunit;

namespace EventPlayer.Test
{
    public class EventPlayerTests
    {
        [Fact]
        public void PerformCommand_ExceptionOnValidationError()
        {
            // init
            var command = new StubPostCommand(false);
            var model = new StubModel {Id = new Id<StubModel>("1"), BoolVal = false};

            // run
            Assert.Throws<Exception>(() => command.ExecuteOn(model));
        }

        [Fact]
        public void ExecuteCommand()
        {
            // init
            const int currentVersion = 0;
            const int expectedVerstion = currentVersion + 1;
            var model = new StubModel
            {
                Id = new Id<StubModel>(),
                BoolVal = false,
                LatestVersion = currentVersion
            };
            var expected = new StubNoChangeEvent(model.Id) {Version = expectedVerstion};
            var command = new StubCommand();

            // run
            var actual = command.ExecuteOn(model);
            Assert.Equal(expected.Id.Value, actual.Id.Value);
            Assert.Equal(expected.Version, actual.Version);
            Assert.Equal(1, actual.Version);

            Assert.Equal(false, model.BoolVal);
        }

        [Fact]
        public void ExecuteCommand_PlayChanges()
        {
            // init
            const int currentVersion = 0;
            const int expectedVerstion = currentVersion + 1;
            var model = new StubModel
            {
                Id = new Id<StubModel>("1"),
                BoolVal = false,
                LatestVersion = currentVersion
            };
            var expected = new StubChangeEvent(model.Id, true) {Version = expectedVerstion};
            var command = new StubPostCommand(true);

            // run
            var actualEvt = command.ExecuteOn(model);
            Assert.Equal(expected.Id.Value, actualEvt.Id.Value);
            Assert.Equal(expected.Version, actualEvt.Version);
            Assert.Equal(expectedVerstion, actualEvt.Version);

            var player = new Player<StubModel>();
            player.PlayFor(model, actualEvt);

            Assert.Equal(true, model.BoolVal);
            Assert.Equal(expectedVerstion, model.LatestVersion);
            Assert.Equal(actualEvt.Version, model.LatestVersion);

            player.PlayFor(model, actualEvt);

            Assert.Equal(true, model.BoolVal);
            Assert.Equal(expectedVerstion, model.LatestVersion);
            Assert.Equal(actualEvt.Version, model.LatestVersion);
        }

        [Fact]
        public void ExecuteCommand_PlayChanges_PlaySubscribedChanges()
        {
            // init
            const int currentVersion = 0;
            const int expectedVerstion = currentVersion + 1;
            var model = new StubModel
            {
                Id = new Id<StubModel>("1"),
                BoolVal = false,
                LatestVersion = currentVersion
            };

            StubSubscribedEvent.OnPlaybackApply(eventData => changeModel => changeModel.BoolVal = !eventData.boolVal);
            StubSubscribedEvent.OnPlaybackApply(eventData => changeModel => changeModel.BoolVal = eventData.boolVal);

            var expected = new StubSubscribedEvent(model.Id, true) {Version = expectedVerstion};

            var command = new StubSubscribedCommand(true);

            // run
            var actualEvt = (StubSubscribedEvent) command.ExecuteOn(model);

            Assert.Equal(expected.Id.Value, actualEvt.Id.Value);
            Assert.Equal(expected.Version, actualEvt.Version);
            Assert.Equal(expectedVerstion, actualEvt.Version);

            var player = new Player<StubModel>();
            player.PlayFor(model, actualEvt);

            Assert.Equal(true, model.BoolVal);
            Assert.Equal(expectedVerstion, model.LatestVersion);
            Assert.Equal(actualEvt.Version, model.LatestVersion);

            player.PlayFor(model, actualEvt);

            Assert.Equal(true, model.BoolVal);
            Assert.Equal(expectedVerstion, model.LatestVersion);
            Assert.Equal(actualEvt.Version, model.LatestVersion);
        }

        [Fact]
        public void ExecuteCommand_PlayChangesNow()
        {
            // init
            const int currentVersion = 0;
            const int expectedVerstion = currentVersion + 1;
            var model = new StubModel
            {
                Id = new Id<StubModel>("1"),
                BoolVal = false,
                LatestVersion = currentVersion
            };
            var expected = new StubChangeEvent(model.Id, true) {Version = expectedVerstion};
            var command = new StubPostCommand(new Player<StubModel>(), true);

            // run
            var actualEvt = command.ExecuteOn(model);
            Assert.Equal(expected.Id.Value, actualEvt.Id.Value);
            Assert.Equal(expected.Version, actualEvt.Version);
            Assert.Equal(expectedVerstion, actualEvt.Version);

            Assert.Equal(true, model.BoolVal);
            Assert.Equal(expectedVerstion, model.LatestVersion);
            Assert.Equal(actualEvt.Version, model.LatestVersion);
        }

        [Fact]
        public void DeleteModelCommand()
        {
            // init
            const int currentVersion = 1;
            var model = new StubModel
            {
                Id = new Id<StubModel>("1"),
                BoolVal = false,
                LatestVersion = currentVersion
            };
            var command = new PlayDeleteCommand<StubModel>();
            var expected = new PlayDeletedEvent<StubModel>(model.Id);

            // run
            var actual = command.ExecuteOn(model);
            Assert.Equal(expected.Id.Value, actual.Id.Value);
            Assert.Equal(expected.GetType(), actual.GetType());
        }
    }
}