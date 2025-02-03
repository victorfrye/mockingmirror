import {
  Image,
  Caption1,
  Card,
  CardFooter,
  CardHeader,
  Subtitle2,
  Switch,
  SwitchOnChangeData,
  Title1,
  makeStyles,
  tokens,
} from '@fluentui/react-components';
import useDarkMode from '@mockingmirror/hooks/useDarkMode';
import MirrorCamera from '@mockingmirror/components/MirrorCamera';

import ClownEmoji from '/images/clown.svg';
import { ChangeEvent, useState } from 'react';
import MirrorDisplay from '@mockingmirror/components/MirrorDisplay';

const useStyles = makeStyles({
  frame: {
    display: 'flex',
    minHeight: 'calc(100vh - (var(--spacingVerticalXXXL) * 2))',
    '@media screen and (max-width: 576px)': {
      minHeight: '100vh',
      padding: `${tokens.spacingVerticalNone}`,
    },
    padding: `${tokens.spacingVerticalXXXL}`,
  },
  mirrorCard: {
    display: 'flex',
    flexDirection: 'column',
    width: '100%',
    boxShadow: tokens.shadow64,
    padding: `${tokens.spacingVerticalL} ${tokens.spacingHorizontalL}`,
  },
  header: {
    alignItems: 'center',
    padding: `${tokens.spacingVerticalXL} ${tokens.spacingHorizontalXXL} ${tokens.spacingVerticalNone}`,
  },
  title: {
    margin: `${tokens.spacingVerticalNone} ${tokens.spacingHorizontalSNudge}`,
  },
  tagline: {
    color: tokens.colorBrandForeground2,
    margin: `${tokens.spacingVerticalNone} ${tokens.spacingHorizontalSNudge}`,
  },
  mirrorContainer: {
    display: 'flex',
    gap: tokens.spacingVerticalXL,
    margin: `${tokens.spacingVerticalXL} ${tokens.spacingVerticalNone}`,
    flexDirection: 'column',
    '@media screen and (min-width: 900px)': {
      flexDirection: 'row',
    },
  },
  footer: {
    display: 'flex',
    '@media screen and (max-width: 576px)': {
      flexDirection: 'column',
    },
    justifyItems: 'center',
    marginTop: 'auto',
    padding: `${tokens.spacingVerticalNone} ${tokens.spacingHorizontalXL} ${tokens.spacingVerticalXL}`,
  },
  switch: {
    marginTop: 'auto',
    marginBottom: 'auto',
    '@media screen and (max-width: 576px)': {
      marginLeft: 'auto',
      marginRight: 'auto',
      padding: `${tokens.spacingVerticalSNudge} ${tokens.spacingHorizontalL} ${tokens.spacingVerticalNone}`,
    },
    padding: `${tokens.spacingVerticalNone} ${tokens.spacingHorizontalM}`,
  },
  copyright: {
    marginTop: 'auto',
    marginBottom: 'auto',
    marginLeft: 'auto',
    '@media screen and (max-width: 576px)': {
      marginRight: 'auto',
      padding: `${tokens.spacingVerticalSNudge} ${tokens.spacingHorizontalL} ${tokens.spacingVerticalNone}`,
    },
    flexWrap: 'wrap',
    padding: `${tokens.spacingVerticalNone} ${tokens.spacingHorizontalL}`,
  },
});

const Mirror = () => {
  const styles = useStyles();
  const { isDark, onDarkModeToggled } = useDarkMode();
  const [image, setImage] = useState<string | null>(null);

  const handleDarkModeToggled = (
    _event: ChangeEvent<HTMLInputElement>,
    data: SwitchOnChangeData
  ) => {
    onDarkModeToggled(data.checked);
  };

  const onImageChanged = (newImage: string) => setImage(newImage);

  return (
    <div className={styles.frame}>
      <Card className={styles.mirrorCard}>
        <CardHeader
          className={styles.header}
          image={
            <Image
              as="img"
              src={ClownEmoji}
              alt="a clown to represent the mirror"
              height={72}
              width={72}
            />
          }
          header={
            <Title1 as="h1" wrap={false} className={styles.title}>
              Mocking Mirror
            </Title1>
          }
          description={
            <Subtitle2 as="em" className={styles.tagline}>
              Mirror, mirror on the screen, who knows nothing about AI at all?
            </Subtitle2>
          }
        />

        <div className={styles.mirrorContainer}>
          <MirrorCamera handleImageChanged={onImageChanged} />

          {image && <MirrorDisplay image={image} />}
        </div>

        <CardFooter className={styles.footer}>
          <Switch
            checked={isDark}
            onChange={handleDarkModeToggled}
            label={isDark ? 'Dark Mode' : 'Light Mode'}
            className={styles.switch}
          />

          <Caption1 as="p" align="end" block className={styles.copyright}>
            © Victor Frye {new Date().getFullYear()}
          </Caption1>
        </CardFooter>
      </Card>
    </div>
  );
};

export default Mirror;
