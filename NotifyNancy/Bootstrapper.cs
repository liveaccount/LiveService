namespace NotifyNancy
{
    using Nancy;
    using WebSocketSharp;
    using WebSocketSharp.Server;

    public class Bootstrapper : DefaultNancyBootstrapper
    {
        private readonly WebSocketServer ws;

        public Bootstrapper()
        {
            ws = new WebSocketServer("ws://liveservice.apphb.com");
            ws.AddWebSocketService<Laputa>("/Laputa");
        }

        protected override void ApplicationStartup(Nancy.TinyIoc.TinyIoCContainer container, Nancy.Bootstrapper.IPipelines pipelines)
        {
            ws.Start();

            base.ApplicationStartup(container, pipelines);
        }
    }

    public class Laputa : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            var msg = e.Data == "BALUS"
                      ? "I've been balused already..."
                      : "I'm not available now.";

            Send(msg);
        }
    }
}