import { FC, useEffect, useRef } from 'react';

import {
  Button,
  Card,
  CardFooter,
  Image,
  makeStyles,
} from '@fluentui/react-components';
import { Camera16Regular } from '@fluentui/react-icons';

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
  const videoRef = useRef<HTMLVideoElement | null>(null);
  const canvasRef = useRef<HTMLCanvasElement | null>(null);

  const startCamera = async () => {
    try {
      const stream = await navigator.mediaDevices.getUserMedia({ video: true });

      if (videoRef.current) {
        videoRef.current.srcObject = stream;
        videoRef.current.play();
      }
    } catch (error) {
      console.error('Error accessing the camera: ', error);
    }
  };

  const takeSnapshot = () => {
    if (canvasRef.current && videoRef.current) {
      const context = canvasRef.current.getContext('2d');

      canvasRef.current.width = videoRef.current.videoWidth;
      canvasRef.current.height = videoRef.current.videoHeight;

      if (context) {
        context.drawImage(
          videoRef.current,
          0,
          0,
          canvasRef.current.width,
          canvasRef.current.height
        );
        const dataUrl = canvasRef.current.toDataURL('image/png');

        handleImageChanged(dataUrl);
      }
    }
  };

  useEffect(() => {
    startCamera();

    return () => {
      if (videoRef.current?.srcObject) {
        // eslint-disable-next-line react-hooks/exhaustive-deps
        const stream = videoRef.current.srcObject as MediaStream;

        stream.getTracks().forEach((track) => track.stop());
      }
    };
  }, []);

  return (
    <Card className={styles.card} appearance="filled-alternative">
      {/* eslint-disable @typescript-eslint/no-explicit-any */}
      <Image
        as={'video' as any}
        ref={videoRef as any}
        className={styles.video}
        shadow
        shape="rounded"
      />
      {/* eslint-enable @typescript-eslint/no-explicit-any */}

      <canvas ref={canvasRef} className={styles.canvas} />

      <CardFooter
        className={styles.footer}
        action={
          <Button
            as="button"
            className={styles.button}
            icon={<Camera16Regular />}
            onClick={takeSnapshot}
            appearance="primary"
          >
            Capture Image
          </Button>
        }
      ></CardFooter>
    </Card>
  );
};

export default MirrorCamera;
