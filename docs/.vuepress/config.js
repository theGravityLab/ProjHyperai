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
  base: 'ProjHyperai',
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
            '0.0.about.md', '0.1.how-to-read.md', '0.2.contribute.md',]
        },
        {
          title: '接触 HyperaiShell',
          collapsable: true,
          children: ['1.0.about.md', '1.1.deploy.md', '1.2.install-plugins.md']
        },
        {
          title: '开发 HyperaiShell 插件',
          collapsable: true,
          children: ['5.0.about.md']
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
