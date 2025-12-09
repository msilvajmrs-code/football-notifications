using System;
using System.Collections.Generic;
using System.Text;
using Visma.Technical.Core.Contracts;
using Visma.Technical.Core.Features.ProcessFootballEvent;

namespace Visma.Technical.Core.Features.PublishFootballEvent.InputHandlers
{
    public interface IInputHandler
    {
        Notification HandleInput(EventInput eventInput, Game game);
    }
}
