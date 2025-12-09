using Microsoft.Extensions.DependencyInjection;
using Visma.Technical.Core.Features.PublishFootballEvent.InputHandlers;

namespace Visma.Technical.Core.Features.ProcessFootballEvent.InputHandlers
{
    public static class InputHandlersAsKeyedServices
    {
        public static void RegisterInputHandlersAsKeyedServices(this IServiceCollection services)
        {
            services.AddKeyedScoped<IInputHandler, GoalInputHandler>(nameof(EventType.Goal));
            services.AddKeyedScoped<IInputHandler, CardInputHandler>(nameof(EventType.YellowCard));
            services.AddKeyedScoped<IInputHandler, CardInputHandler>(nameof(EventType.RedCard));
            services.AddKeyedScoped<IInputHandler, GeneralInputHandler>(nameof(EventType.Commentary));
        }
        
    }
}
