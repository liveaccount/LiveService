namespace NancyApp
{
    using System;
    using System.Collections.Concurrent;

    public interface IHandlerBagDictionary
    {
        HandlerBag GetOrAdd(String id);
    }

    public class HandlerBagDictionary : IHandlerBagDictionary
    {
        private readonly ConcurrentDictionary<String, HandlerBag> dictionary;

        public HandlerBagDictionary()
        {
            dictionary = new ConcurrentDictionary<string, HandlerBag>();
        }

        public HandlerBag GetOrAdd(String id)
        {
            return dictionary.GetOrAdd(id, n => new HandlerBag(n));
        }
    }
}