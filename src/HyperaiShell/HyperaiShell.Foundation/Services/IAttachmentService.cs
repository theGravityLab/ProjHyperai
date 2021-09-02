using System;
using Hyperai.Relations;

namespace HyperaiShell.Foundation.Services
{
    public interface IAttachmentService
    {
        void Attach<T>(T ins, RelationModel toWhom);

        void Detach<T>(RelationModel toWhom);

        T Retrieve<T>(RelationModel fromWhom);

        ForAttachmentUpdateScope<T> For<T>(RelationModel model, out T ins, Func<T> generator);
    }
}
