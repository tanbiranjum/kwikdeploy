using KwikDeploy.Application.Common.Interfaces;

namespace KwikDeploy.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
