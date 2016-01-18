namespace NancyApp.Modules
{
    using System.Threading;

    using Nancy;

    public class ModuleIndex : NancyModule
    {
        public ModuleIndex()
        {
            Get["/"] = _ =>
            {
                return View["index"];
            };
        }
    }
}