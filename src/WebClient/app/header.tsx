'use client';

import {
  CardHeader,
  Image,
  Subtitle2,
  Title1,
  makeStyles,
  tokens,
} from '@fluentui/react-components';

const useStyles = makeStyles({
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
});

const Header = () => {
  const styles = useStyles();

  return (
    <CardHeader
      className={styles.header}
      image={
        <Image
          as="img"
          src="/images/clown.svg"
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
  );
};

export default Header;
