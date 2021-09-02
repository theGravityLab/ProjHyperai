using System.Threading.Tasks;
using Hyperai.Events;
using HyperaiShell.Foundation.Bots;

namespace HyperaiShell.Foundation.Services
{
    public interface IBotService
    {
        IBotCollectionBuilder Builder { get; }

        Task PushAsync(GenericEventArgs args);
    }
}
