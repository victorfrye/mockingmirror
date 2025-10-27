'use client';

import { useCallback, useEffect, useRef } from 'react';

import {
  Body1,
  Card,
  CardFooter,
  Image,
  ProgressBar,
  makeStyles,
  tokens,
} from '@fluentui/react-components';

import { useRoasts } from '@mockingmirror/api';
import Loading from '@mockingmirror/loading';
import MirrorText from '@mockingmirror/mirror/text';

const useStyles = makeStyles({
  card: {
    display: 'flex',
    width: '100%',
    minWidth: '360px',
    '@media screen and (min-width: 900px)': {
      width: `calc(50% - ${tokens.spacingHorizontalXL})`,
    },
  },
  image: {
    width: 'auto',
    height: 'auto',
  },
  footer: {
    display: 'flex',
    flexDirection: 'column',
  },
});

interface RoastDisplayProps {
  image: string;
}

export default function RoastDisplay({ image }: Readonly<RoastDisplayProps>) {
  const styles = useStyles();
  const audioRef = useRef<HTMLAudioElement | null>(null);

  const { roast, error, loading } = useRoasts({ image });

  const playSpeech = useCallback(() => {
    if (audioRef.current) {
      audioRef.current.pause();
    }

    if (roast?.speech) {
      const audio = new Audio(`data:audio/wav;base64,${roast.speech}`);
      audioRef.current = audio;
      audio.play().catch((error) => {
        console.error('Error playing audio: ', error);
      });
    }
  }, [roast]);

  useEffect(() => {
    playSpeech();
  }, [playSpeech]);

  const renderText = useCallback(() => {
    if (loading) {
      return MirrorText.reflection.loading;
    }

    if (error) {
      return MirrorText.reflection.error;
    }

    return roast?.text;
  }, [loading, error, roast]);

  return (
    <Card className={styles.card} appearance="filled-alternative">
      <>
        <Image
          as="img"
          src={image}
          alt="Captured"
          shadow
          shape="rounded"
          className={styles.image}
        />

        <CardFooter className={styles.footer}>
          <Body1 as="p">{renderText()}</Body1>

          <audio ref={audioRef} />

          {loading && <ProgressBar />}
        </CardFooter>
      </>
    </Card>
  );
}
