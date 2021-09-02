# HyperaiShell

开箱即用的 Hyperai Application. ProjHyperai 的一部分.

<!-- PROJECT SHIELDS -->

[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]

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


## 部署机器人 | Deploy

### 视窗系统 | Windows

下载已经编译好的 [Release](https://github.com/theGravityLab/HyperaiShell/releases).

### 林纽克斯 | Linux

参考[手动编译](https://projhyperai.dowob.vip/guide/2.1.deploy/#%E6%89%8B%E5%8A%A8%E7%BC%96%E8%AF%91).

### 配置 | Configuration

HyperaiShell 仅能处理消息, 消息接收发送需要[**适配器**](https://projhyperai.dowob.vip/guide/5.1.knowledge/#%E9%80%82%E9%85%8D%E5%99%A8-iapiclient).


*为保证及时更新, 包括适配器和如何配置等内容请在文档中查看.*

### 目前支持的适配器列表 | Supported Adapters

|平台|媒介|项目|
|--|--|--|
|OICQ|[mirai-api-http](https://github.com/project-mirai/mirai-api-http)|[Ac682.Hyperai.Clients.Mirai](https://github.com/ac682/Ac682.Hyperai.Clients.Mirai)|
|OICQ|[go-cqhttp](https://github.com/Mrs4s/go-cqhttp)|[Ac682.Hyperai.Clients.CQHTTP](https://github.com/ac682/Ac682.Hyperai.Clients.CQHTTP)|
|OICQ|[CQHTTP](https://github.com/richardchien/coolq-http-api)|🈚(你去PR就有了)|
|Telegram|[TG-API](https://core.telegram.org/api)|🈚(你去PR就有了)|

## 插件开发 | Plugin Development

首先安装 nuget 包
```bash
dotnet add package HyperaiShell.Foundation
```

然后阅读[开发文档>>](https://projhyperai.dowob.vip/guide/5.0.about/)

## 截图 | Screenshots

![screenshot](.github/images/screenshot.png)
[![FOSSA Status](https://app.fossa.com/api/projects/git%2Bgithub.com%2FtheGravityLab%2FHyperaiShell.svg?type=shield)](https://app.fossa.com/projects/git%2Bgithub.com%2FtheGravityLab%2FHyperaiShell?ref=badge_shield)

## 引用 | Reference

- [Best README template](https://github.com/shaojintian/Best_README_template/blob/master/README.md)
- [GitHub Emoji Cheat Sheet](https://www.webpagefx.com/tools/emoji-cheat-sheet)
- [Image Shields](https://shields.io)
- [Choose an Open Source License](https://choosealicense.com)
- [Netlify](https://www.netlify.com/)

<!-- links -->
[project-path]:theGravityLab/HyperaiShell
[contributors-shield]: https://img.shields.io/github/contributors/theGravityLab/HyperaiShell?style=for-the-badge
[contributors-url]: https://github.com/theGravityLab/HyperaiShell/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/theGravityLab/HyperaiShell?style=for-the-badge
[forks-url]: https://github.com/theGravityLab/HyperaiShell/network/members
[stars-shield]: https://img.shields.io/github/stars/theGravityLab/HyperaiShell?style=for-the-badge
[stars-url]: https://github.com/theGravityLab/HyperaiShell/stargazers

## License
[![FOSSA Status](https://app.fossa.com/api/projects/git%2Bgithub.com%2FtheGravityLab%2FHyperaiShell.svg?type=large)](https://app.fossa.com/projects/git%2Bgithub.com%2FtheGravityLab%2FHyperaiShell?ref=badge_large)