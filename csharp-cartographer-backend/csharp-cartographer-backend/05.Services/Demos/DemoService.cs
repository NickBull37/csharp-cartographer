using csharp_cartographer_backend._02.Utilities.Providers;

namespace csharp_cartographer_backend._05.Services.Demos
{
    public class DemoService : IDemoService
    {
        public IEnumerable<DemoFile> GetDemoFileData()
        {
            return DemoFileProvider.GetDemoFiles();
        }
    }
}
