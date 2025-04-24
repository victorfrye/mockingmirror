'use client'

import { useState } from 'react';

import {
  makeStyles,
  tokens,
} from '@fluentui/react-components';

import { Camera, Display } from '@mockingmirror/mirror';

const useStyles = makeStyles({
  main: {
    display: 'flex',
    gap: tokens.spacingVerticalXL,
    margin: `${tokens.spacingVerticalXL} ${tokens.spacingVerticalNone}`,
    flexDirection: 'column',
    '@media screen and (min-width: 900px)': {
      flexDirection: 'row',
    },
  },
});

const HomePage = () => {
  const styles = useStyles();
  const [image, setImage] = useState<string | null>(null);

  const onImageChanged = (newImage: string) => setImage(newImage);

  return (
    <main className={styles.main}>
      <Camera handleImageChanged={onImageChanged} />

      {image && <Display image={image} />}
    </main>
  );
};

export default HomePage;
