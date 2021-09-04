using System;
using Hyperai.Relations;
using HyperaiShell.Foundation.Data;
using HyperaiShell.Foundation.Services;
using Microsoft.Extensions.Logging;
using Sentry;
using Attachment = HyperaiShell.App.Models.Attachment;

namespace HyperaiShell.App.Services
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IHub _hub;
        private readonly ILogger _logger;
        private readonly IRepository _repository;

        public AttachmentService(IRepository repository, ILogger<AttachmentService> logger, IHub hub)
        {
            _repository = repository;
            _logger = logger;
            _hub = hub;
        }

        public void Attach<T>(T ins, RelationModel toWhom)
        {
            var transaction = _hub.StartTransaction($"{nameof(HyperaiShell)}-{nameof(AttachmentService)}",
                nameof(Attach)
                , typeof(T).Name);
            var typeName = typeof(T).FullName;
            var first = _repository.Query<Attachment>()
                .Where(x => x.Target == toWhom.Identifier && x.TypeName == typeName).FirstOrDefault();
            if (first != null)
            {
                first.Object = ins;
                _repository.Update(first);
            }
            else
            {
                first = new Attachment
                {
                    Target = toWhom.Identifier,
                    TypeName = typeName,
                    Object = ins
                };
                _repository.Store(first);
            }

            transaction.Finish();
        }

        public void Detach<T>(RelationModel toWhom)
        {
            var transaction = _hub.StartTransaction($"{nameof(HyperaiShell)}-{nameof(AttachmentService)}",
                nameof(Detach)
                , typeof(T).Name);
            var typeName = typeof(T).FullName;
            var first = _repository.Query<Attachment>()
                .Where(x => x.Target == toWhom.Identifier && x.TypeName == typeName).FirstOrDefault();
            if (first != null) _repository.Delete<Attachment>(first.Id);
            transaction.Finish();
        }

        public T Retrieve<T>(RelationModel fromWhom)
        {
            var transaction = _hub.StartTransaction($"{nameof(HyperaiShell)}-{nameof(AttachmentService)}",
                nameof(Retrieve), typeof(T).Name);
            var typeName = typeof(T).FullName;
            var ins = (T)_repository.Query<Attachment>()
                .Where(x => x.Target == fromWhom.Identifier && x.TypeName == typeName).FirstOrDefault()?.Object;
            transaction.Finish();
            return ins;
        }

        public ForAttachmentUpdateScope<T> For<T>(RelationModel model, out T ins, Func<T> generator = null)
        {
            var t = Retrieve<T>(model) ?? (generator ?? (() => default))();
            var scope = new ForAttachmentUpdateScope<T>(this, t, model);
            ins = t;
            return scope;
        }
    }
}
