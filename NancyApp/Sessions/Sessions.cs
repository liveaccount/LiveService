namespace NancyApp.Sessions
{
    using System;
    using System.Collections.Concurrent;

    using Nancy.AspNet.WebSockets;

    public interface ISessions
    {
        Session GetOrAdd(string id);
    }

    public class Sessions : ISessions
    {
        private readonly ConcurrentDictionary<string, Session> sessions;

        public Sessions()
        {
            sessions = new ConcurrentDictionary<string, Session>();
        }

        public Session GetOrAdd(string key)
        {
            return sessions.GetOrAdd(key, k => new Session(k));
        }
    }
}