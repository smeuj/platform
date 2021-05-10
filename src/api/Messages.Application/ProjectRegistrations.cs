using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Nouwan.SmeujPlatform.Messages.Application
{
    public static  class ProjectRegistrations
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            Infrastructure.ProjectRegistrations.Register(services, configuration);
        }
    }
}
