'use client'

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
} from '@fluentui/react-components'
import { useDarkMode } from '../hooks/darkMode'
import MirrorCamera from './MirrorCamera'

import ClownEmoji from '/clown.svg'
import { ChangeEvent, useState } from 'react'
import MirrorImage from './MirrorImage'

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
})

interface Roast {
  image: string
  text: string
}

const Mirror = () => {
  const styles = useStyles()
  const { isDark, onDarkModeToggled } = useDarkMode()
  const [roast, setRoast] = useState<Roast | null>(null)

  const handleDarkModeToggled = (
    _event: ChangeEvent<HTMLInputElement>,
    data: SwitchOnChangeData
  ) => {
    onDarkModeToggled(data.checked)
  }

  const onImageChanged = (newImage: string) => {
    // Send request to backend to get roast

    setRoast({
      image: newImage,
      text: 'You look like a clown',
    })
  }

  return (
    <div className={styles.frame}>
      <Card className={styles.mirrorCard}>
        <CardHeader
          className={styles.header}
          image={
            <Image
              src={ClownEmoji}
              alt="a headstone for that which is dead"
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
              An AI-as-a-service demo application by Victor Frye
            </Subtitle2>
          }
        />

        <div className={styles.mirrorContainer}>
          <MirrorCamera handleImageChanged={onImageChanged} />

          {roast && <MirrorImage image={roast.image} text={roast.text} />}
        </div>

        <CardFooter className={styles.footer}>
          {/* <Socials /> */}
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
  )
}

export default Mirror
