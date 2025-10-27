'use client';

import { ReactNode } from 'react';

import { ColorModeProvider, ThemeProvider } from '@mockingmirror/theme';

interface ProviderTreeProps {
  children: ReactNode;
}

export default function ProviderTree({
  children,
}: Readonly<ProviderTreeProps>) {
  return (
    <ColorModeProvider>
      <ThemeProvider>{children}</ThemeProvider>
    </ColorModeProvider>
  );
}
