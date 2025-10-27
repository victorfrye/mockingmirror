'use client';

import { JSX, useCallback } from 'react';

import {
  Button,
  Tooltip,
  makeStyles,
  tokens,
} from '@fluentui/react-components';

import GitHubSvg from '@mockingmirror/shell/github.svg';
import ShellText from '@mockingmirror/shell/text';
import { useColorMode } from '@mockingmirror/theme';

interface Social {
  text: string;
  link: string;
  styles?: string;
  image: JSX.Element;
}

const useStyles = makeStyles({
  svg: {
    color: tokens.colorNeutralForeground1,
    fill: tokens.colorNeutralForeground1,
    width: '20px',
    height: 'auto',
  },
});

export default function SocialButtons() {
  const styles = useStyles();

  const getSocials = useCallback((): Social[] => {
    return [
      {
        text: ShellText.footer.socials.github,
        link: 'https://github.com/victorfrye/mockingmirror',
        image: (
          <GitHubSvg
            className={styles.svg}
            aria-label={ShellText.footer.socials.github}
          />
        ),
      },
    ];
  }, [styles]);

  const renderButtons = (): JSX.Element[] => {
    return getSocials().map((social) => (
      <Tooltip
        withArrow
        content={social.text}
        relationship="label"
        key={social.text}
      >
        <Button
          icon={social.image}
          as="a"
          appearance="subtle"
          shape="circular"
          size="large"
          href={social.link}
          target="_blank"
          rel="me noreferrer noopener"
        />
      </Tooltip>
    ));
  };

  return renderButtons();
}
