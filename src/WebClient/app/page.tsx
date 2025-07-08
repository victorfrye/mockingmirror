'use client';

import { useState } from 'react';

import { makeStyles, tokens } from '@fluentui/react-components';

import RoastDisplay from '@mockingmirror/roast-display';
import VideoCamera from '@mockingmirror/video-camera';

const useStyles = makeStyles({
  main: {
    display: 'flex',
    gap: tokens.spacingVerticalXL,
    margin: `auto ${tokens.spacingHorizontalL}`,
    justifyItems: 'center',
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
      <VideoCamera handleImageChanged={onImageChanged} />

      {image && <RoastDisplay image={image} />}
    </main>
  );
};

export default HomePage;
