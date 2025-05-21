'use client';

import { useMemo } from 'react';

import useFetch from '@mockingmirror/use-fetch';

const baseUrl = process.env['NEXT_PUBLIC_API_BASEURL'];

export interface RoastRequest {
  imageBytes: string;
}

export interface RoastResponse {
  completionText: string;
  speechBytes?: string;
}

export function usePostRoast(image: string) {
  const request: RoastRequest = useMemo(
    () => ({
      imageBytes: image.split('base64,').pop() ?? '',
    }),
    [image]
  );

  const { data, error, loading } = useFetch<RoastResponse>(
    `${baseUrl}/roasts`,
    {
      method: 'post',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(request),
    }
  );

  return { response: data, error, loading };
}
