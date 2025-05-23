import { ReactNode } from 'react';

import { Metadata } from 'next';

import Frame from '@mockingmirror/frame';
import '@mockingmirror/globals.css';
import { DarkModeProvider, ThemeProvider } from '@mockingmirror/theme';

export const metadata: Metadata = {
  metadataBase: new URL('https://mockingmirror.com'),
  title: 'Mocking Mirror | Telling you how you really look',
  description:
    'Mocking Mirror is the only mirror that tells you how you really look: like a clown.',
  keywords: [
    'mocking mirror',
    'mirror',
    'clown',
    'mocking',
    'funny',
    'joke',
    'humor',
    'AI',
    'artificial intelligence',
    'reflection',
    'roast',
  ],
  icons: ['images/clown.svg'],
  authors: {
    name: 'Victor Frye',
    url: 'https://victorfrye.com/',
  },
  alternates: {
    canonical: '/',
  },
};

export const RootLayout = ({
  children,
}: Readonly<{
  children: ReactNode;
}>) => {
  return (
    <html lang="en">
      <body>
        <div id="root">
          <DarkModeProvider>
            <ThemeProvider>
              <Frame>{children}</Frame>
            </ThemeProvider>
          </DarkModeProvider>
        </div>
      </body>
    </html>
  );
};

export default RootLayout;
