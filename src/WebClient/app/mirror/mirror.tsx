'use client';

import { useState } from 'react';

import { makeStyles, tokens } from '@fluentui/react-components';

import RoastDisplay from '@mockingmirror/mirror/roast-display';
import VideoCamera from '@mockingmirror/mirror/video-camera';

const useStyles = makeStyles({
  container: {
    display: 'flex',
    maxWidth: '100%',
    flexDirection: 'column',
    '@media screen and (min-width: 900px)': {
      flexDirection: 'row',
    },
    gap: tokens.spacingVerticalXL,
  },
});

const Mirror = () => {
  const styles = useStyles();
  const [image, setImage] = useState<string | null>(null);

  const onImageChanged = (newImage: string) => setImage(newImage);

  return (
    <div className={styles.container}>
      <VideoCamera handleImageChanged={onImageChanged} />

      {image && <RoastDisplay image={image} />}
    </div>
  );
};

export default Mirror;
