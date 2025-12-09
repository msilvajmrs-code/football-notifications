using System;
using System.Collections.Generic;
using System.Text;

namespace Visma.Technical.Core.Features.PublishFootballEvent
{
    public class Notification
    {
        public required string GameDescription { get; set; }
        public required string Score { get; set; }
        public required string Message { get; set; }
    }
}
