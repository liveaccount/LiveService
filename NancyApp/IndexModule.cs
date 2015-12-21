namespace NancyApp
{
    using System;
    
    using Nancy;

    using WebSocketSharp;
    using WebSocketSharp.Server;

    public class IndexModule : NancyModule
    {
        private readonly HttpServer server;

        public IndexModule()
        {
            server = new HttpServer();
            server.AddWebSocketService<Test>("/test");

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
                    foreach (var path in server.WebSocketServices.Paths)
                        message += String.Format("- {0}{1}", path, "<br>");
                }

                return message;
            };
        }
    }

    public class Test : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            var msg = e.Data == "Test"
                      ? "Test OK."
                      : "Test FAILED...";

            Send(msg);
        }
    }
}