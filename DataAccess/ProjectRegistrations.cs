using Microsoft.Extensions.DependencyInjection;
using Nouwan.Smeuj.DataAccess.Repositories;

namespace Nouwan.Smeuj.DataAccess
{
    public  class ProjectRegistrations
    {
        public void Register(IServiceCollection services)
        {
            services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
            services.AddSingleton<IMessageRepository, MessageRepository>();
        }
    }
}
