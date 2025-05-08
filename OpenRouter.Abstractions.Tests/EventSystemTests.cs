using System;
using OpenRouter.Abstractions;
using Xunit;

namespace OpenRouter.Abstractions.Tests
{
    public class EventSystemTests
    {
        private class DummyRequest : IRequestModel { }
        private class DummyResponse : IResponseModel { }

        [Fact]
        public void RequestEventArgs_ConstructsCorrectly()
        {
            var req = new DummyRequest();
            var args = new RequestEventArgs(req);
            Assert.Equal(req, args.Request);
            Assert.True((DateTime.UtcNow - args.TimestampUtc).TotalSeconds < 5);
        }

        [Fact]
        public void ResponseEventArgs_ConstructsCorrectly()
        {
            var resp = new DummyResponse();
            var args = new ResponseEventArgs(resp);
            Assert.Equal(resp, args.Response);
            Assert.True((DateTime.UtcNow - args.TimestampUtc).TotalSeconds < 5);
        }

        [Fact]
        public void StreamingEventArgs_ConstructsCorrectly()
        {
            var data = new { Value = 42 };
            var args = new StreamingEventArgs(data);
            Assert.Equal(data, args.Data);
            Assert.True((DateTime.UtcNow - args.TimestampUtc).TotalSeconds < 5);
        }

        [Fact]
        public void ErrorEventArgs_ConstructsCorrectly()
        {
            var ex = new InvalidOperationException("fail");
            var args = new ErrorEventArgs(ex);
            Assert.Equal(ex, args.Exception);
            Assert.True((DateTime.UtcNow - args.TimestampUtc).TotalSeconds < 5);
        }

        [Fact]
        public void ProgressEventArgs_ConstructsCorrectly()
        {
            var args = new ProgressEventArgs(77, "Working");
            Assert.Equal(77, args.ProgressPercentage);
            Assert.Equal("Working", args.StatusMessage);
            Assert.True((DateTime.UtcNow - args.TimestampUtc).TotalSeconds < 5);
        }

        [Fact]
        public void IOpenRouterEventNotifier_EventSignaturesAreCorrect()
        {
            // Compile-time check: can wire up all events
            var notifier = new DummyNotifier();
            notifier.RequestSent += (s, e) => { };
            notifier.ResponseReceived += (s, e) => { };
            notifier.StreamingUpdate += (s, e) => { };
            notifier.ErrorOccurred += (s, e) => { };
            notifier.ProgressChanged += (s, e) => { };
        }

        #pragma warning disable CS0067 // Event is never used
        private class DummyNotifier : IOpenRouterEventNotifier
        {
            public event EventHandler<RequestEventArgs>? RequestSent;
            public event EventHandler<ResponseEventArgs>? ResponseReceived;
            public event EventHandler<StreamingEventArgs>? StreamingUpdate;
            public event EventHandler<ErrorEventArgs>? ErrorOccurred;
            public event EventHandler<ProgressEventArgs>? ProgressChanged;
        }
        #pragma warning restore CS0067
    }
}
