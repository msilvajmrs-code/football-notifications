using System;
using System.Collections.Generic;
using System.Text;

namespace Visma.Technical.Core.Infrastructure.Messaging
{
    public class RabbitMqConfiguration
    {
        public string HostName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
