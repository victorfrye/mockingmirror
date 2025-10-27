'use client';

import { useMemo } from 'react';

import { RoastRequest, RoastResponse } from '@mockingmirror/api/roasts/types';
import useFetch from '@mockingmirror/api/use-fetch';

interface UseRoastsProps {
  image: string;
}

export default function useRoasts({ image }: UseRoastsProps) {
  const request: RoastRequest = useMemo(
    () => ({
      imageBytes: image?.split('base64,').pop() ?? '',
    }),
    [image]
  );

  const init: RequestInit = useMemo(() => {
    return {
      method: 'post',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(request),
    };
  }, [request]);

  const { data, error, loading } = useFetch<RoastResponse>(`/api/roasts`, init);

  const roast = useMemo(() => {
    if (!data) {
      return undefined;
    }

    return {
      text: data.completionText,
      speech: data.speechBytes,
    };
  }, [data]);

  return { roast, error, loading };
}
