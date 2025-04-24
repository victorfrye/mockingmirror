'use client';

import * as React from 'react';
import {
  SSRProvider,
  RendererProvider,
  createDOMRenderer,
  renderToStyleElements,
} from '@fluentui/react-components';
import { useServerInsertedHTML } from 'next/navigation';

const SsrRendererProvider = ({
  children,
}: Readonly<{ children: React.ReactNode }>) => {
  const [renderer] = React.useState(() => createDOMRenderer());
  const didRenderRef = React.useRef(false);

  useServerInsertedHTML(() => {
    if (didRenderRef.current) {
      return;
    }

    didRenderRef.current = true;

    return (
      <>
      {renderToStyleElements(renderer)}
      </>
    );
  });

  return (
    <RendererProvider renderer={renderer}>
      <SSRProvider>
        {children}
      </SSRProvider>
    </RendererProvider>
  );
}

export default SsrRendererProvider;
