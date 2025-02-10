import { FC, useCallback, useEffect, useMemo, useRef, useState } from 'react';

import {
  Body1,
  Card,
  CardFooter,
  Image,
  Spinner,
  makeStyles,
} from '@fluentui/react-components';
import useFetch from '@mockingmirror/hooks/useFetch';

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

interface Roast {
  text: string;
  speech: string | null;
}

interface RoastRequest {
  imageBytes: string;
}

interface RoastResponse {
  completionText: string;
  speechBytes: string | null;
}

interface MirrorDisplayProps {
  image: string;
}

const MirrorDisplay: FC<MirrorDisplayProps> = ({ image }) => {
  const styles = useStyles();
  const audioRef = useRef<HTMLAudioElement | null>(null);
  const [roast, setRoast] = useState<Roast | null>(null);

  const request: RequestInit = useMemo(
    () => ({
      method: 'post',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        imageBytes: image.split('base64,').pop(),
      } as RoastRequest),
    }),
    [image]
  );
  const { data, error, loading } = useFetch<RoastResponse>('/roasts', request);

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
    if (data) {
      setRoast({
        text: data.completionText,
        speech: data.speechBytes,
      });
    }
  }, [data]);

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
};

export default MirrorDisplay;
