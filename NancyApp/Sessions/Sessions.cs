namespace NancyApp.Sessions
{
    using System;
    using System.Collections.Concurrent;

    using Nancy.AspNet.WebSockets;

    public interface ISessions
    {
        Session GetOrAdd(String id);
    }

    public class Sessions : ISessions
    {
        private readonly ConcurrentDictionary<string, Session> sessions;

        public Sessions()
        {
            sessions = new ConcurrentDictionary<string, Session>();
        }

        public Session GetOrAdd(String key)
        {
            return sessions.GetOrAdd(key, k => new Session(k));
        }
    }
}