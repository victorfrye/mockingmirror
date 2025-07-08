'use client';

import { ChangeEvent } from 'react';

import {
  Caption1,
  CardFooter,
  Divider,
  Switch,
  SwitchOnChangeData,
  makeStyles,
  tokens,
} from '@fluentui/react-components';

import Socials from '@mockingmirror/socials';
import { useDarkMode } from '@mockingmirror/theme';

const useStyles = makeStyles({
  footer: {
    display: 'flex',
    '@media screen and (max-width: 576px)': {
      flexDirection: 'column',
      rowGap: tokens.spacingVerticalS,
    },
    justifyItems: 'center',
    padding: `${tokens.spacingVerticalNone} ${tokens.spacingHorizontalXL} ${tokens.spacingVerticalXL}`,
  },
  divider: {
    flex: '0 1 auto',
    margin: `${tokens.spacingVerticalXXL} ${tokens.spacingHorizontalNone} ${tokens.spacingVerticalXXL}`,
  },
  switch: {
    marginTop: 'auto',
    marginBottom: 'auto',
    '@media screen and (max-width: 576px)': {
      marginLeft: 'auto',
      marginRight: 'auto',
      padding: `${tokens.spacingVerticalNone} ${tokens.spacingHorizontalL} ${tokens.spacingVerticalNone}`,
    },
  },
  copyright: {
    marginTop: 'auto',
    marginBottom: 'auto',
    marginLeft: 'auto',
    '@media screen and (max-width: 576px)': {
      marginRight: 'auto',
      padding: `${tokens.spacingVerticalMNudge} ${tokens.spacingHorizontalL} ${tokens.spacingVerticalNone}`,
    },
    flexWrap: 'wrap',
    padding: `${tokens.spacingVerticalNone} ${tokens.spacingHorizontalL}`,
  },
});

export default function Footer() {
  const styles = useStyles();
  const _today: Date = new Date();

  const { isDark, onDarkModeToggled } = useDarkMode();

  const handleDarkModeToggled = (
    _event: ChangeEvent<HTMLInputElement>,
    data: SwitchOnChangeData
  ) => {
    onDarkModeToggled(data.checked);
  };

  return (
    <>
      <Divider appearance="subtle" inset className={styles.divider} />

      <CardFooter className={styles.footer}>
        <Socials />
        <Switch
          checked={isDark}
          onChange={handleDarkModeToggled}
          label={isDark ? 'Dark Mode' : 'Light Mode'}
          className={styles.switch}
        />

        <Caption1 as="span" align="end" block className={styles.copyright}>
          © Victor Frye {_today.getFullYear()}
        </Caption1>
      </CardFooter>
    </>
  );
}
