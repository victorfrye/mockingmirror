import {
  Body1,
  Card,
  CardFooter,
  Image,
  makeStyles,
} from '@fluentui/react-components';
import { FC } from 'react';

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

interface MirrorImageProps {
  image: string;
  text: string;
}

const MirrorImage: FC<MirrorImageProps> = ({ image, text }) => {
  const styles = useStyles();

  return (
    <Card className={styles.card} appearance="filled-alternative">
      <Image
        as="img"
        src={image}
        alt="Captured"
        shadow
        shape="rounded"
        className={styles.image}
      />

      <CardFooter>
        <Body1>{text}</Body1>
      </CardFooter>
    </Card>
  );
};

export default MirrorImage;
