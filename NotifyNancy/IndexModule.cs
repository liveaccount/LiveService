namespace NotifyNancy
{
    using Nancy;

    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = parameters =>
            {
                return Bootstrapper.Path();// View["index"];
            };
        }
    }
}