namespace NancyApp
{
    using System;
    
    using Nancy;

    using WebSocketSharp;
    using WebSocketSharp.Server;
    using System.Web;

    public class IndexModule : NancyModule
    {
        private readonly HttpServer server;

        public IndexModule()
        {
            server = new HttpServer();
            server.AddWebSocketService<Test>("/");

            Get["/"] = parameters =>
            {
                return View["index"];
            };

            Get["/websocket"] = parameters =>
            {
                try
                {
                    server.Start();
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }

                var message = String.Empty;

                if (server.IsListening)
                {
                    message = String.Format("Server started on port {0}, and providing services:{1}", server.Port, "<br>");
                    foreach (var host in server.WebSocketServices.Hosts)
                        message += String.Format("- {0}{1}", host.Path, "<br>");
                }

                return message;
            };
        }
    }

    public class Test : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            Send("Test OK.");
        }
    }
}