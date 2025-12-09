using System.Threading;
using System.Threading.Tasks;

namespace Visma.Technical.Core.Contracts
{
    public interface IMqPublisher
    {
        Task PublishAsync<T>(string queue, T message);
    }
}