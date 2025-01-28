import { Body1, Button, Card, CardFooter, Image, makeStyles, tokens } from '@fluentui/react-components';
import { Camera16Regular } from '@fluentui/react-icons';
import React from 'react';

const useStyles = makeStyles({
  container: {
    display: 'flex',
    gap: tokens.spacingVerticalXL,
    margin: `${tokens.spacingVerticalXL} ${tokens.spacingVerticalNone}`,
    flexDirection: 'column',
    '@media screen and (min-width: 900px)': {
      flexDirection: 'row',
    },
  },
  videoCard: {
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
  videoButton: {
  },

  canvas: {
    display: 'none',
  },
  capturedImage: {

  },
  image: {
    width: 'auto',
    height: 'auto',
  },
});

const CameraCapture = () => {
  const styles = useStyles();
  const [imageSrc, setImageSrc] = React.useState<string | null>(null);
  const videoRef = React.useRef<HTMLVideoElement | null>(null);
  const canvasRef = React.useRef<HTMLCanvasElement | null>(null);

  const startCamera = async () => {
    try {
      const stream = await navigator.mediaDevices.getUserMedia({ video: true });

      if (videoRef.current) {
        videoRef.current.srcObject = stream;
        videoRef.current.play();
      }
    } catch (error) {
      console.error("Error accessing the camera: ", error);
    }
  };

  const takeSnapshot = () => {
    if (canvasRef.current && videoRef.current) {
      const context = canvasRef.current.getContext('2d');

      canvasRef.current.width = videoRef.current.videoWidth;
      canvasRef.current.height = videoRef.current.videoHeight;

      if (context) {
        context.drawImage(videoRef.current, 0, 0, canvasRef.current.width, canvasRef.current.height);
        const dataUrl = canvasRef.current.toDataURL('image/png');

        setImageSrc(dataUrl);
      }
    }
  };

  React.useEffect(() => {
    startCamera();

    return () => {
      if (videoRef.current && videoRef.current.srcObject) {
        const stream = videoRef.current.srcObject as MediaStream;
        stream.getTracks().forEach(track => track.stop());
      }
    };
  }, []);

  return (
    <div className={styles.container}>
      <Card className={styles.videoCard} appearance="filled-alternative">
        {/* eslint-disable-next-line @typescript-eslint/no-explicit-any */}
        <Image as={"video" as any} ref={videoRef as any} className={styles.video} shadow shape='rounded' />
        <canvas ref={canvasRef} className={styles.canvas} />

        <CardFooter

          action={
            <Button
              className={styles.videoButton}
              icon={<Camera16Regular />}
              onClick={takeSnapshot}
            >
              Capture Image
            </Button>
          }>

        </CardFooter>
      </Card>

      {imageSrc &&
        <Card className={styles.videoCard} appearance="filled-alternative">
          <Image as="img" src={imageSrc} alt="Captured" shadow shape="rounded" className={styles.image} />

          <CardFooter>
            <Body1>
              Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.
            </Body1>
          </CardFooter>
        </Card>
      }
    </div>
  );
};

export default CameraCapture;
