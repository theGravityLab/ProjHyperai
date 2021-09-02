using System;
using Hyperai.Relations;

namespace HyperaiShell.Foundation.Services
{
    public sealed class ForAttachmentUpdateScope<T> : IDisposable
    {
        private readonly T _instance;
        private readonly IAttachmentService _service;
        private readonly RelationModel _toWhom;

        private bool isDisposed;

        public ForAttachmentUpdateScope(IAttachmentService service, T instance, RelationModel toWhom)
        {
            _service = service;
            _instance = instance;
            _toWhom = toWhom;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool isDisposing)
        {
            if (!isDisposed && isDisposing)
            {
                if (_instance != null) _service.Attach(_instance, _toWhom);
                isDisposed = true;
            }
        }
    }
}
