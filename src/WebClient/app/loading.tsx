'use client';

import { Spinner, makeStyles } from '@fluentui/react-components';

const useStyles = makeStyles({
  spinner: {
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'center',
    margin: 'auto',
  },
});

interface LoadingProps {
  message?: string;
}

export default function Loading({ message }: Readonly<LoadingProps>) {
  const styles = useStyles();

  return (
    <Spinner
      label={message ?? 'Loading...'}
      size="huge"
      className={styles.spinner}
    />
  );
}
