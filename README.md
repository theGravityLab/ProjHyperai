# ProjHyperai

某机器人开发框架和机器人服务的项目总仓库.

<!-- PROJECT SHIELDS -->

[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]

<!-- PROJECT LOGO -->

<p align="center">
  <a href="https://github.com/theGravityLab/ProjHyperai">
    <img src="docs/images/sucks.png" alt="Logo" width="180" height="117">
  </a>
</p>


  <h3 align="center">ProjHyperai</h3>
  <p align="center">
    QQ/TG 机器人开发在这入门
    <br />
    <a href="https://projhyperai.dowob.vip"><strong>本项目的文档 »</strong></a>
    <br />
    <br />
    <a href="https://jq.qq.com/?_wv=1027&k=oygKDvyw">加入群聊</a>
    ·
    <a href="https://github.com/theGravityLab/ProjHyperai/issues">报告问题</a>
    ·
    <a href="https://github.com/theGravityLab/ProjHyperai/issues">提供建议</a>
  </p>


## 导航 | Guide

ProjHyperai 具有以下子项目

1. [Hyperai](https://github.com/theGravityLab/Hyperai) - Hyperai 项目就是 Hyperai 机器人开发框架所依赖的基础设施, 包含事件定义到消息实现, 以及用于构造 Hyperai Application 的定义及默认实现.
2. [Hyperai.Units](https://github.com/theGravityLab/Hyperai.Units) - 可集成到 Hyperai Application 的模块, 提供类似 MVC 中 Controller 的开发体验.
3. [HyperaiShell](https://github.com/theGravityLab/HyperaiShell) -  个人自己实现的 Hyperai Application , 集成了大部分模块和服务, 开箱即用, 用插件扩展. 除非你觉得能做的更好, 不然就用它吧. 

#### 如何选择一个子项目并继续开发? | How To Begin

- 我想自己构造机器人程序或集成 Hyperai 到已有的程序中 => Hyperai
- 除了上一条, 我还想有 Units 模块 => Hyperai + Hyperai.Units
- 我不想写代码, 只想白嫖 => HyperaiShell + 来自社区的插件
- 不想写那么多胶水代码, 只想专注于机器人的特定功能 => HyperaiShell + 自己写的插件
- 我全都要 => HyperaiShell + 来自社区的插件 + 自己写的插件

## 开发体验 | Development Experience

*指 HyperaiShell 插件的开发体验, 反正前几个都没人用…*

#### 消息构造 | MessageChain Construction

```csharp
public override void OnMemberJoin(object sender, GroupMemberJoinEventArgs args)
{
    var chain = $"[hyper.at({args.Who.Identity})]TA来了!".MakeMessageChain(); // 特殊码是 Hyperai 内定义的, 不管在哪都是同一个实现, 放心写. 为什么要加个前缀 hyper? 为了区分其他第三方实现, 同时便于肉眼/编辑器搜索识别.
    args.Group.SendAsync(chain); // 等不等待都无所谓
}

public override void OnMemberLeave(object sender, GroupMemberLeaveEventArgs args)
{
    if(args.IsKicked)
    {
        var chain = MessageChain.Construct(new Plain($"{args.Who.DisplayName}({args.Who.Identity})滚蛋了!"));
        args.Group.SendAsync(chain).Wait(); // 那就等吧
    }else
    {
        var builder = new MessageChainBuilder();
        builder.AddPlain($"{args.Who.DisplayName}({args.Who.Identity})");
    	builder.AddPlain("TA走了...");
        var messageId = args.Group.SendAsync(builder.Build()).GetAwaiter().GetResult(); // 发送消息会返回 MessageId, 为了获取它还是等一下吧.
    }
}
```

#### 消息处理 | Message Handling

```csharp
// MessageChain 本质是 IEnumerabe<MessageComponent>,
// 能用所有的 Linq 语句.
// 这就够了.

// 不够? 需要一个文本表示?
var hypertext = MessageChain.Construct(new At(10000), new Plain("我俏丽吗!")).Flatten();
// -> hypertext = "[hyper.at(10000)]我俏丽吗!"
// 这是 string.MakeMessageChain 的逆过程, 内部用的是 IMessageChain(Formatter/Parser)
// 你永远可以相信这种操作输出是具有可依赖性的

// "我能不能用 MessageChain.ToString 来文本化?"
// 可以, 但是结果是不确定的, 不建议把输出拿来做文本处理
// MessageChain.ToString 内部调用了 MessageComponent.ToString
// 而 ToString 是用来展示对象结构的, 不是给程序"读"的.
// 你可以 Console.WriteLine(messageChain.ToString()) 但不可以 messageChain.ToString().IndexOf("at")
// 因为后者不一定会把 @某人 转换为文本"at", 甚至可能转换为"@了某个人"
```

#### 发送消息: 和好友的友好互动 | Message Sending

```csharp
private async Task DelayedTask(Friend friend)
{
    var chain = "我俏丽吗?(y/n)".MakeMessageChain();
    await friend.SendAsync(chain);
    friend.Await(FuckReply);
    
    void FuckReply(MessageContext context)
    {
        if(context.Message.Flatten().ToLower() == "y")
        {
            await context.ReplyAsync("你说说我哪里俏丽.".MakeMessageChain());
            // 在该语境下可以确定 context.User 一定是 Friend
            ((Friend)context.User).Await(context => context.ReplyAsync("这可是你说的.".MakeMessageChain()).Wait());
        }else
        {
            await context.ReplyAsync("???".MakeMessageChain());
        }
    }
}
```

#### 事件处理 | Event Handling

##### 顶层接收器 | Top-receivers

```csharp
void Foo(IApiClient client)
{
    client.On<FriendMessageEventArgs>(new DefaultEventHandler(args => args.User.SendPlainAsync("[自动回复]正忙, 稍后回复.").Wait());
}
```

##### 中间件 | Middlewares

```csharp
void Bar(IHyperaiApplicationBuilder app)
{
    app.Use<FooBarMiddleware>();
}

// 是的, 中间件是具名的, 没有匿名中间件. 既然要实现中间件构造注入, 那就让它具名吧.
class FooBarMiddleware: IMiddleware
{
    private readonly ILogger _logger;
    public FooBarMiddleware(ILogger<FooBarMiddleware> logger)
    {
        _logger = logger;
    }
    public bool Run(GenericEventArgs args)
    {
        _logger.LogInformation("I got it!");
    }
}
```

##### 实现一个 Bots 模块中定义的 Bot | Custom Bots

```csharp
void Fuck(IBotCollectionBuilder builder)
{
    // 你需要先注册这个 bot 它才会起效.
    builder.Add<FuckingBot>();
}

class FuckingBot: BotBase
{
    public override void OnFriendMessage(object sender, FriendMessageEventArgs args)
    {
        args.User.SendPlainAsync("sodayo").Wait();
    }
}
```

##### 实现一个 Units 模块中定义的 Unit | Units and Actions

```csharp
void Suck()
{
    // 因为 Unit 是自动寻找的, 所以不需要手动添加
}

class UnitSucks: UnitBase
{
    [Receive(MessageEventType.Group)]
    // [Extract("*[hyper.at(@Self.Identity)]*")] // 中间的 @Self.Idenetity 并没有实现...
    [Extract("*[hyper.at({who})]*")] // 但是你可以这么写, 然后判断一下
    public async Task Milky(long who, Group where, Self me)
    {
        if(who == me.Identity)
        {
            await where.SendPlainAsync("叫👴干嘛?");
        }
    }
}
```

#### 读配置 | Retrieving Configuration

*配置是只读的, 就别想着怎么写了; 就算写也只是 in-memory, 不会保存到文件的*

##### 读 HyperaiShell 的程序配置 | Configuration of HyperaiShell

```csharp
// 只能依赖注入
// "哪些地方能依赖注入?" 看文档https://projhyperai.dowob.vip, 里面有写
class Rock
{
    private readonly IConfiguration _configuration;
    public Rock(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    private void Peek()
    {
        if(_configuration["Application:SelectedClient"].ToLower().Contains("mirai"))
        {
            // Aha, 你在用 mirai!
        }
    }
}
```

##### HyperaiShell 的插件读 HyperaiShell 插件的配置(也就是自己的配置) | Configuration of the Plugin Itself

```csharp
class APlugin: PluginBase
{
    private readonly IConfiguration _configuration;
    public PluginEntry()
    {
        // PluginBase 不支持任何注入, 也不能访问外部服务, 唯一获取服务的方式是 PluginBase.Context, 里面提供了已经配制好的服务.
        _configuration = Context.Configuration;
    }
}

class AnUnit: UnitBase
{
    private readonly IConfiguration _configuration;
    public AnUnit(IPluginConfiguration<APlugin> configuration)
    {
        // UnitBase 可以访问服务, BotBase 同理.
        // IPluginConfiguration 需要泛型参数来指定是哪个插件的私有配置
        _configuration = configuration.Value;
    }
}
```

#### 访问数据库 | Access to LiteDb

```csharp
// 和上一部分是一样的, HyperaiShell 中的数据库由 LiteDb 提供
// 对应的类型为 IRepository 和为插件用的 IPluginRepository<TPlugin>
```

## 使用本仓库 | Contributing to this Repo

本仓库包含多个子项目, 并提供了一个 Visual Studio 解决方案用于同时控制子项目.

**请不要在 `master` 分支上开发.**

#### 写代码 | Coding

不同子项目之间依赖包而不是项目, 想要快速应用某个子项目的更改到其他项目请将该子项目打包并**添加到本地的离线包源**.

##### 克隆 | Clone

```bash
git clone --recursice https://github.com/theGravityLab/ProjHyperai.git
git checkout dev
```

#### 写文档 | Documents

使用文本编辑器打开 `./docs` 目录即可开始工作. 提交 pr 即可应用修改.

// TODO: 不知道有没有 pr merged 的 trigger, 有请告知.

##### 克隆 | Clone

```bash
git clone https://github.com/theGravityLab/ProjHyperai.git
git checkout dev
```

## 引用 | Reference

- [Best README template](https://github.com/shaojintian/Best_README_template/blob/master/README.md)
- [GitHub Emoji Cheat Sheet](https://www.webpagefx.com/tools/emoji-cheat-sheet)
- [Image Shields](https://shields.io)
- [Choose an Open Source License](https://choosealicense.com)
- [Netlify](https://www.netlify.com/)

<!-- links -->
[project-path]:theGravityLab/ProjHyperai
[contributors-shield]: https://img.shields.io/github/contributors/theGravityLab/ProjHyperai?style=for-the-badge
[contributors-url]: https://github.com/theGravityLab/ProjHyperai/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/theGravityLab/ProjHyperai?style=for-the-badge
[forks-url]: https://github.com/theGravityLab/ProjHyperai/network/members
[stars-shield]: https://img.shields.io/github/stars/theGravityLab/ProjHyperai?style=for-the-badge
[stars-url]: https://github.com/theGravityLab/ProjHyperai/stargazers