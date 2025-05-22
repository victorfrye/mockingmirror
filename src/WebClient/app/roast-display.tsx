'use client';

import { useCallback, useEffect, useRef, useState } from 'react';

import {
  Body1,
  Card,
  CardFooter,
  Image,
  Spinner,
  makeStyles,
} from '@fluentui/react-components';

import { Roast } from '@mockingmirror/types';
import useRoastApi from '@mockingmirror/use-roast-api';

const useStyles = makeStyles({
  card: {
    display: 'flex',
    width: '100%',
    minWidth: '360px',
    '@media screen and (min-width: 900px)': {
      width: '50%',
    },
  },
  image: {
    width: 'auto',
    height: 'auto',
  },
});

interface RoastDisplayProps {
  image: string;
}

export default function RoastDisplay({ image }: Readonly<RoastDisplayProps>) {
  const styles = useStyles();
  const audioRef = useRef<HTMLAudioElement | null>(null);
  const [roast, setRoast] = useState<Roast | null>(null);

  const { postRoast, response, error, loading } = useRoastApi();

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
    if (loading) {
      postRoast(image);
    }
  }, [image, loading, postRoast]);

  useEffect(() => {
    if (response) {
      setRoast({
        text: response.completionText,
        speech: response.speechBytes,
      });
    }
  }, [response]);

  useEffect(() => {
    playSpeech();
  }, [playSpeech]);

  return (
    <Card className={styles.card} appearance="filled-alternative">
      {loading ? (
        <Spinner label={'Roasting...'} size="extra-large" />
      ) : (
        <>
          <Image
            as="img"
            src={image}
            alt="Captured"
            shadow
            shape="rounded"
            className={styles.image}
          />

          <CardFooter>
            <Body1 as="p">
              {error
                ? 'Hmmm. Something went wrong and it is probably your fault.'
                : roast?.text}
            </Body1>

            <audio ref={audioRef} />
          </CardFooter>
        </>
      )}
    </Card>
  );
}
