import {
  Body1,
  Card,
  CardFooter,
  Image,
  makeStyles,
  Spinner,
} from '@fluentui/react-components';
import { FC, useEffect, useMemo, useState } from 'react';
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
  prompt: string | null;
}

interface MirrorImageProps {
  image: string;
}

const MirrorImage: FC<MirrorImageProps> = ({ image }) => {
  const styles = useStyles();

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

  const [roast, setRoast] = useState<Roast | null>(null);

  const playSpeech = (speech: string) => {
    const audio = new Audio(`data:audio/wav;base64,${speech}`);
    audio.play().catch((error) => {
      console.error('Error playing audio: ', error);
    });
  };

  useEffect(() => {
    if (data !== null) {
      setRoast({
        text: data.completionText,
        speech: data.speechBytes,
      });
    }
  }, [data]);

  useEffect(() => {
    if (roast?.speech) {
      playSpeech(roast?.speech);
    }
  }, [roast]);

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
          </CardFooter>
        </>
      )}
    </Card>
  );
};

export default MirrorImage;
