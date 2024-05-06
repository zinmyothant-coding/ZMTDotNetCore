using Microsoft.Extensions.Configuration;
using ZMTDotNetCore.RestApiWithNLayer.Feature.Blog;

namespace ZMTDotNetCore.RestApiWithNLayer
{
    public  static class Startup
    {
         
        public static void AddServices(this IServiceCollection service)
        {
            service.AddSingleton<BL_Blog>();
            service.AddSingleton<DA_Blog>();
        }
    }
}
