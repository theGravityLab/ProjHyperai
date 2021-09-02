# Hyperai.Units

为 Hyperai Application 提供 Units 服务, 将处理对象细化到消息.

<!-- PROJECT SHIELDS -->

[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![FOSSA Status](https://app.fossa.com/api/projects/git%2Bgithub.com%2FtheGravityLab%2FHyperai.Units.svg?type=shield)](https://app.fossa.com/projects/git%2Bgithub.com%2FtheGravityLab%2FHyperai.Units?ref=badge_shield)

<!-- PROJECT LOGO -->

<p align="center">
  <a href="https://github.com/theGravityLab/ProjHyperai">
    <img src="https://github.com/theGravityLab/ProjHyperai/raw/master/docs/images/sucks.png" alt="Logo" width="180" height="117">
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


## 注册服务 | Registering Units service

安装包
```bash
dotnet add package Hyperai.Units
```

在一个已有的 `IHyperaiApplicationBuilder` 对象中添加服务
```csharp
var builder = MagicalBox.Take<IHyperaiApplicationBuilder>();
// => new HyperaiApplicationBuilder()

builder.Services.AddUnits();

var app = builder.Build();

var service = app.Provider.GetRequiredService<IUnitService>();
service.SearchForUnits();

app.Run();
```

## 编写一个 Unit | Make a class a Unit

在任意地方创建一个类, 名字任意
```csharp
public class Asshole
{
    [Receive(MessageEventType.Group)]
    [Extract("!fuck {who}")]
    [CheckTicket("ability.to.fuck")]
    public async Task Fuck(Member sender, Group group, long who)
    {
        FuckRecord record = null;
	    using(sender.For<FuckRecord>(() => new FuckRecord(0), out record))
	    {
		    await group.SendAsync($"[hyper.at({sender.Identity})] fucked [hyper.at({who})] the {++record.Count}th time.").MakeMessageChain());
	    }
    }
}
```

#### 解析参数 | Parsing Arguments

如你所见 `ExtractAttribute` 可以在消息中解析出特定文本, UnitService 会把该文本转换为所需要的值类型或 `MessageChain`.

#### 权限标注 | That's not my job, sir. It's yours.

你看到了 `CheckTicketAttribute`, 但是这并不是本项目提供的功能. 但你可以使用 `FilterByAttribute` 来实现它.

#### 方法注入 | Method Injection

是的, 所需的参数用形参来注入.

形参中可以有 `UnitBase.Context` 中的成员(使用类型来匹配), 也可以是 `ExtractAttribute` 中大括号内的参数(使用名字来识别).

## 引用 | Reference

- [Best README template](https://github.com/shaojintian/Best_README_template/blob/master/README.md)
- [GitHub Emoji Cheat Sheet](https://www.webpagefx.com/tools/emoji-cheat-sheet)
- [Image Shields](https://shields.io)
- [Choose an Open Source License](https://choosealicense.com)
- [Netlify](https://www.netlify.com/)

<!-- links -->
[project-path]:theGravityLab/Hyperai.Units
[contributors-shield]: https://img.shields.io/github/contributors/theGravityLab/Hyperai.Units?style=for-the-badge
[contributors-url]: https://github.com/theGravityLab/Hyperai.Units/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/theGravityLab/Hyperai.Units?style=for-the-badge
[forks-url]: https://github.com/theGravityLab/Hyperai.Units/network/members
[stars-shield]: https://img.shields.io/github/stars/theGravityLab/Hyperai.Units?style=for-the-badge
[stars-url]: https://github.com/theGravityLab/Hyperai.Units/stargazers

## License
[![FOSSA Status](https://app.fossa.com/api/projects/git%2Bgithub.com%2FtheGravityLab%2FHyperai.Units.svg?type=large)](https://app.fossa.com/projects/git%2Bgithub.com%2FtheGravityLab%2FHyperai.Units?ref=badge_large)