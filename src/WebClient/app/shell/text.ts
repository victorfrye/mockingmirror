const ShellText = {
  header: {
    title: 'Mocking Mirror',
    tagline: 'Mirror, mirror on the screen, who knows nothing about AI at all?',
  },
  footer: {
    socials: {
      github: 'victorfrye/mockingmirror | GitHub',
    },
    toggleColor(mode: string) {
      return `Toggle ${mode} mode`;
    },
    byline: 'Made with 💙 by Victor Frye',
    privacy: 'Privacy',
    copyright(year: number) {
      return `© Victor Frye ${year}`;
    },
  },
};

export default ShellText;
