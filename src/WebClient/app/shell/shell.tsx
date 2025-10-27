'use client';

import { ReactNode } from 'react';

import { Card, makeStyles, tokens } from '@fluentui/react-components';

import Footer from '@mockingmirror/shell/footer';
import Header from '@mockingmirror/shell/header';

const useStyles = makeStyles({
  shell: {
    display: 'flex',
    minHeight: `calc(100vh - (${tokens.spacingVerticalXXXL} * 2))`,
    '@media screen and (max-width: 576px)': {
      minHeight: '100vh',
      padding: tokens.spacingVerticalNone,
    },
    padding: tokens.spacingVerticalXXXL,
  },
  card: {
    display: 'flex',
    flexDirection: 'column',
    width: '100%',
    boxShadow: tokens.shadow64,
    padding: tokens.spacingVerticalL,
  },
  main: {
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    justifyItems: 'center',
    padding: `${tokens.spacingVerticalNone} ${tokens.spacingHorizontalL}`,
    margin: `auto ${tokens.spacingHorizontalNone}`,
  },
  tabs: {
    margin: `${tokens.spacingVerticalSNudge} ${tokens.spacingHorizontalNone} ${tokens.spacingHorizontalMNudge}`,
  },
  link: {
    textDecoration: 'none',
    ':hover': {
      textDecoration: 'none',
    },
    ':focus': {
      textDecoration: 'none',
    },
    ':active': {
      textDecoration: 'none',
    },
  },
});

interface ShellProps {
  children: ReactNode;
}

export default function Shell({ children }: Readonly<ShellProps>) {
  const styles = useStyles();

  return (
    <div className={styles.shell}>
      <Card className={styles.card}>
        <Header />
        <main className={styles.main}>{children}</main>
        <Footer />
      </Card>
    </div>
  );
}
