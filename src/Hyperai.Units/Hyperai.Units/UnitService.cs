using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Hyperai.Events;
using Hyperai.Messages;
using Hyperai.Messages.ConcreteModels;
using Hyperai.Relations;
using Hyperai.Services;
using Hyperai.Units.Attributes;
using Microsoft.Extensions.Logging;

namespace Hyperai.Units
{
    public class UnitService : IUnitService
    {
        private readonly IMessageChainFormatter _formatter;
        private readonly ILogger<UnitService> _logger;
        private readonly IMessageChainParser _parser;

        private readonly IServiceProvider _provider;

        private readonly Dictionary<Signature, ConcurrentQueue<QueueEntry>> invaders = new();
        private IEnumerable<ActionEntry> entries;

        public UnitService(IServiceProvider provider, IMessageChainFormatter formatter, IMessageChainParser parser,
            ILogger<UnitService> logger)
        {
            _provider = provider;
            _formatter = formatter;
            _logger = logger;
            _parser = parser;
        }

        public void Handle(MessageContext context)
        {
            var flag = false;
            foreach (var channel in invaders.Keys)
                if (context.User switch
                {
                    Member member => channel.Match(member), Friend friend => channel.Match(friend), _ => false
                })
                {
                    if (invaders[channel].TryDequeue(out var action))
                    {
                        if (DateTime.Now < action.CreatedAt + action.Timeout)
                        {
                            action.Action(context);
                            flag = true;
                        }
                    }
                    else
                    {
                        invaders.Remove(channel);
                    }

                    break;
                }

            if (!flag)
            {
                var ava = GetEntries().Where(x => x.Type == context.Type);
                foreach (var e in ava) HandleOne(e, context);
            }
        }

        public void WaitOne(Signature channel, ActionDelegate action, TimeSpan timeout)
        {
            if (!invaders.ContainsKey(channel)) invaders.Add(channel, new ConcurrentQueue<QueueEntry>());
            invaders[channel].Enqueue(new QueueEntry(action, timeout));
        }

        public void SearchForUnits()
        {
            var ent = new List<ActionEntry>();
            foreach (var ass in AppDomain.CurrentDomain.GetAssemblies().Where(p => !p.IsDynamic))
            {
                var types = ass.GetExportedTypes().Where(x => !x.IsAbstract && x.IsSubclassOf(typeof(UnitBase)));
                foreach (var type in types)
                {
                    var methods = type.GetMethods().Where(x => x.IsPublic && !x.IsStatic && !x.IsAbstract);
                    foreach (var method in methods)
                    {
                        var att = method.GetCustomAttribute<ReceiveAttribute>();
                        if (att == null) continue;

                        var entry = new ActionEntry(att.Type, method, type, 0);
                        ent.Add(entry);
                    }
                }
            }

            entries = ent;
        }

        public IEnumerable<ActionEntry> GetEntries()
        {
            return entries;
        }

        public void HandleOne(ActionEntry entry, MessageContext context)
        {
            if (entry.State is int errorCount and >= 3)
            {
                if (errorCount == 3)
                    _logger.LogWarning("An Action has met its error limit and has been disabled: {Entry}", entry);

                return;
            }

            #region Extract Check

            var extract = entry.Action.GetCustomAttribute<ExtractAttribute>();
            var dict = new Dictionary<string, MessageChain>();

            if (extract != null)
            {
                var text = _formatter.Format(context.Message.AsReadable());
                if (extract.TrimSpaces)
                {
                    var rawChars = text.ToArray();
                    var output = new char[rawChars.Length];
                    var j = 0;
                    foreach (var rawChar in rawChars)
                        if (j == 0 && rawChar != ' ' || j > 0 && (output[j - 1] != ' ' || rawChar != ' '))
                        {
                            output[j] = rawChar;
                            j++;
                        }

                    while (j > 0 && output[j - 1] == ' ') j--;

                    text = new string(output[..j]);
                }

                var match = extract.Pattern.Match(text);
                if (match.Success)
                {
                    var names = extract.Names.ToArray();
                    for (var i = 1; i < match.Groups.Count; i++)
                        dict.Add(names[i - 1], _parser.Parse(match.Groups[i].Value));
                }
                else
                {
                    return;
                }
            }

            #endregion Extract Check

            #region Filter Check

            var filterBys = entry.Action.GetCustomAttributes(typeof(FilterByAttribute), false);
            string failureMessage = null;
            var pass = filterBys.All(x =>
            {
                var filter = (FilterByAttribute) x;
                if (!filter.Filter.Check(context))
                {
                    failureMessage = filter.FailureMessage;
                    return false;
                }

                return true;
            });
            if (!pass)
            {
                if (failureMessage != null)
                {
                    var chain = new MessageChain(new MessageElement[]
                        {new Plain(entry.Action.Name + ": " + failureMessage)});
                    context.ReplyAsync(chain).Wait();
                }

                return;
            }

            #endregion Filter Check

            InvokeOne(entry, context, dict);
        }

        private void InvokeOne(ActionEntry entry, MessageContext context, Dictionary<string, MessageChain> names)
        {
            var paras = entry.Action.GetParameters();
            var paList = new object[paras.Length];
            try
            {
                foreach (var para in paras)
                    if (names.ContainsKey(para.Name!))
                        // pattern
                        paList[para.Position] = para.ParameterType switch
                        {
                            _ when para.ParameterType == typeof(string) => _formatter.Format(names[para.Name!]),
                            _ when para.ParameterType == typeof(MessageChain) => names[para.Name!],
                            _ when typeof(MessageElement).IsAssignableFrom(para.ParameterType) => names[para.Name!]
                                .FirstOrDefault(x => x.GetType() == para.ParameterType),
                            // _ when para.ParameterType == typeof(Member) && names[para.Name].Any(x
                            // => x is At) => GetMember(((At)names[para.Name].First(x => x is At)).TargetId),
                            // unit 不应该即时计算
                            _ when para.ParameterType != typeof(string) && para.ParameterType.IsValueType =>
                                typeof(Convert).GetMethod("To" + para.ParameterType.Name, new[] {typeof(string)})
                                    ?.Invoke(null, new object[] {_formatter.Format(names[para.Name!])}),
                            _ => throw new NotImplementedException("Pattern type not supported: " +
                                                                   para.ParameterType.FullName)
                        };
                    else
                        // context
                        paList[para.Position] = para.ParameterType switch
                        {
                            _ when para.ParameterType == typeof(string) => _formatter.Format(context.Message),
                            _ when para.ParameterType == typeof(Group) => context.Group,
                            _ when para.ParameterType == typeof(Self) => context.Me,
                            _ when para.ParameterType == typeof(MessageChain) => context.Message,
                            _ when para.ParameterType == typeof(DateTime) => context.SentAt,
                            _ when para.ParameterType == typeof(IApiClient) => context.Client,
                            _ when para.ParameterType == typeof(MessageEventType) => context.Type,
                            _ when para.ParameterType.IsInstanceOfType(context.User) => context.User,
                            _ => throw new NotImplementedException("Context type not supported: " +
                                                                   para.ParameterType.FullName)
                        };
            }
            catch (Exception e)
            {
                if (entry.State is int cnt) entry.State = cnt + 1;
                _logger.LogError(e, "Failed to configure context of Unit Action");
                return;
            }

            var unit = UnitFactory.Instance.CreateUnit(entry.Unit, context, _provider);
            _logger.LogInformation("Action hit: {Entry}", entry);

            #region IF_ASYNC_ACTION

            var attr = entry.Action.GetCustomAttribute<AsyncStateMachineAttribute>();
            if (attr != null)
            {
                var task = entry.Action.Invoke(unit, paList.ToArray()) as Task;
                // 有些方法签名为 public async void _(...)，得不到task，就无法捕获异常
                task?.ContinueWith(t =>
                {
                    if (t.Exception != null)
                    {
                        _logger.LogError(t.Exception, "Exception occurred while executing unit action asynchronously");
                        if (entry.State is int count) entry.State = count + 1;
                    }
                    else
                    {
                        if (entry.State is int count)
                        {
                            entry.State = count - 1;
                            if (count < 0) entry.State = 0;
                        }
                    }
                });
            }
            else
            {
                try
                {
                    entry.Action.Invoke(unit, paList.ToArray());

                    if (entry.State is int count)
                    {
                        entry.State = count - 1;
                        if (count < 0) entry.State = 0;
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Exception caught while executing unit action synchronously");
                    if (entry.State is int count) entry.State = count + 1;
                }
            }

            #endregion
        }

        private readonly struct QueueEntry
        {
            public readonly ActionDelegate Action;
            public readonly TimeSpan Timeout;
            public readonly DateTime CreatedAt;

            public QueueEntry(ActionDelegate action, TimeSpan timeout)
            {
                Action = action;
                Timeout = timeout;
                CreatedAt = DateTime.Now;
            }
        }
    }
}
