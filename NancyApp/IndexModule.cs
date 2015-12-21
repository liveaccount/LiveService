namespace NancyApp
{
    using Nancy;

    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = parameters =>
            {
                return View["index"];
            };

            Get["/Laputa"] = parameters =>
            {
                return 200;
            };
        }
    }
}