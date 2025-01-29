import {
  Button,
  Card,
  CardFooter,
  Image,
  makeStyles,
} from '@fluentui/react-components';
import { Camera16Regular } from '@fluentui/react-icons';
import { useEffect, FC, useRef } from 'react';

const useStyles = makeStyles({
  card: {
    display: 'flex',
    width: '100%',
    minWidth: '360px',
    '@media screen and (min-width: 900px)': {
      width: '50%',
    },
  },
  video: {
    minWidth: '320px',
    width: '100%',
    height: 'auto',
  },
  canvas: {
    display: 'none',
  },
  button: {},
  footer: {
    display: 'flex',
    justifyItems: 'center',
  },
});

interface MirrorCameraProps {
  handleImageChanged: (src: string) => void;
}

const MirrorCamera: FC<MirrorCameraProps> = ({ handleImageChanged }) => {
  const styles = useStyles();
  const video = useRef<HTMLVideoElement | null>(null);
  const canvas = useRef<HTMLCanvasElement | null>(null);

  const startCamera = async () => {
    try {
      const stream = await navigator.mediaDevices.getUserMedia({ video: true });

      if (video.current) {
        video.current.srcObject = stream;
        video.current.play();
      }
    } catch (error) {
      console.error('Error accessing the camera: ', error);
    }
  };

  const takeSnapshot = () => {
    if (canvas.current && video.current) {
      const context = canvas.current.getContext('2d');

      canvas.current.width = video.current.videoWidth;
      canvas.current.height = video.current.videoHeight;

      if (context) {
        context.drawImage(
          video.current,
          0,
          0,
          canvas.current.width,
          canvas.current.height
        );
        const dataUrl = canvas.current.toDataURL('image/png');

        handleImageChanged(dataUrl);
      }
    }
  };

  useEffect(() => {
    startCamera();

    return () => {
      if (video.current?.srcObject) {
        // eslint-disable-next-line react-hooks/exhaustive-deps
        const stream = video.current.srcObject as MediaStream;

        stream.getTracks().forEach((track) => track.stop());
      }
    };
  }, []);

  return (
    <Card className={styles.card} appearance="filled-alternative">
      {/* eslint-disable @typescript-eslint/no-explicit-any */}
      <Image
        as={'video' as any}
        ref={video as any}
        className={styles.video}
        shadow
        shape="rounded"
      />
      {/* eslint-enable @typescript-eslint/no-explicit-any */}

      <canvas ref={canvas} className={styles.canvas} />

      <CardFooter
        className={styles.footer}
        action={
          <Button
            className={styles.button}
            icon={<Camera16Regular />}
            onClick={takeSnapshot}
          >
            Capture Image
          </Button>
        }
      ></CardFooter>
    </Card>
  );
};

export default MirrorCamera;
