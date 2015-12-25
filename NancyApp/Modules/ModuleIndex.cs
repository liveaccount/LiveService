namespace NancyApp.Modules
{
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