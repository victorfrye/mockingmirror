'use client';

import {
  CardHeader,
  Divider,
  Image,
  Subtitle2,
  Title1,
  makeStyles,
  tokens,
} from '@fluentui/react-components';

import ShellText from '@mockingmirror/shell/text';

const useStyles = makeStyles({
  header: {
    alignItems: 'center',
    padding: `${tokens.spacingVerticalXL} ${tokens.spacingHorizontalXXL} ${tokens.spacingVerticalNone}`,
  },
  title: {
    margin: `${tokens.spacingVerticalNone} ${tokens.spacingHorizontalSNudge}`,
  },
  tagline: {
    color: tokens.colorNeutralForeground2,
    margin: `${tokens.spacingVerticalNone} ${tokens.spacingHorizontalSNudge}`,
  },
  divider: {
    flex: '0 1 auto',
    margin: `${tokens.spacingVerticalXXL} ${tokens.spacingHorizontalNone} ${tokens.spacingVerticalXXL}`,
  },
});

export default function Header() {
  const styles = useStyles();

  return (
    <div>
      <CardHeader
        className={styles.header}
        image={
          <Image
            as="img"
            src="/assets/clown.svg"
            alt="a clown"
            height={72}
            width={72}
          />
        }
        header={
          <Title1 as="span" wrap={false} className={styles.title}>
            {ShellText.header.title}
          </Title1>
        }
        description={
          <Subtitle2 as="em" className={styles.tagline}>
            {ShellText.header.tagline}
          </Subtitle2>
        }
      />
      <Divider appearance="subtle" inset className={styles.divider} />
    </div>
  );
}
