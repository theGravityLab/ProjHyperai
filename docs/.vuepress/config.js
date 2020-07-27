const resolve = require("vuepress-theme-hope/resolve");
const { description } = require('../../package')

module.exports = resolve({
  /**
   * Ref：https://v1.vuepress.vuejs.org/config/#title
   */
  title: 'ProjHyperai Docs',
  /**
   * Ref：https://v1.vuepress.vuejs.org/config/#description
   */
  description: "The document for Hyperai",
  /**
   * Extra tags to be injected to the page HTML `<head>`
   *
   * ref：https://v1.vuepress.vuejs.org/config/#head
   */
  head: [
    ['meta', { name: 'theme-color', content: '#3eaf7c' }],
    ['meta', { name: 'apple-mobile-web-app-capable', content: 'yes' }],
    ['meta', { name: 'apple-mobile-web-app-status-bar-style', content: 'black' }]
  ],

  /**
   * Theme configuration, here is the default theme configuration for VuePress.
   *
   * ref：https://v1.vuepress.vuejs.org/theme/default-theme-config.html
   */
  themeConfig: {
    comment: {
      type: "valine",
      appId: "5E98xJ67OtxxHBQDiqP451T3-gzGzoHsz",
      appKey: "unWUiD4X3MthP60Do5XokH9G",
    },
    markdown: {
      enableAll: true,
    },
    repo: '',
    editLinks: false,
    docsDir: '',
    editLinkText: '',
    lastUpdated: false,
    nav: [
      {
        text: '指南',
        link: '/guide/0.0.about'
      },
      {
        text: 'Github',
        link: 'https://github.com/theGravityLab/ProjHyperai'
      }
    ],
    sidebar: {
      '/guide/': [
        {
          title: '接触 ProjHyperai',
          collapsable: false,
          children: [
            '0.0.about', '0.1.how-to-read', '0.2.contribute',]
        },
        {
          title: '接触 Hyperai',
          collapsable: true,
          children: ['1.0.about.md']
        },
        {
          title: '接触 HyperaiShell',
          collapsable: true,
          children: ['2.0.about', '2.1.deploy', '2.2.install-plugins']
        },
        {
          title: '开发 HyperaiShell 插件',
          collapsable: true,
          children: [
            '5.0.about',
            '5.1.knowledge',
            '5.2.preparation',
            '5.3.first-plugin',
            '5.4.dependencyinjection',
            '5.5.bots',
            '5.6.units',
            '5.7.code-layer',
            '5.8.space',
            '5.9.attachments',
            '5.10.authorization',
            '5.11.hyperaishell-functions']
        },
        {
          title: '我把整篇文档看完了，但程序依旧不工作',
          collapsable: false,
          children: ['6.0.about']
        }
      ]
    }
  },

  /**
   * Apply plugins，ref：https://v1.vuepress.vuejs.org/zh/plugin/
   */
  plugins: [
    '@vuepress/plugin-back-to-top',
    '@vuepress/plugin-medium-zoom',
  ]
});
