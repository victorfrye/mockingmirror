import { ReactNode } from 'react';

import { Metadata } from 'next';

import '@mockingmirror/globals.css';
import ProviderTree from '@mockingmirror/provider-tree';
import { Shell } from '@mockingmirror/shell';

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
  icons: ['assets/clown.svg'],
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
          <ProviderTree>
            <Shell>{children}</Shell>
          </ProviderTree>
        </div>
      </body>
    </html>
  );
};

export default RootLayout;
