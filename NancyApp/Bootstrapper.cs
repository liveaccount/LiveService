namespace NancyApp
{
    using Nancy;
    using Nancy.TinyIoc;
    using Nancy.Bootstrapper;

    public class Bootstrapper : DefaultNancyBootstrapper
    {
        private static IHandlerBagDictionary handlerBagDictionary = new HandlerBagDictionary();

        public static IHandlerBagDictionary HandlerBagDictionary
        {
            get { return handlerBagDictionary; }
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
        }
    }
}