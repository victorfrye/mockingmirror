import { useState, useEffect, useCallback } from 'react';

const baseUrl = import.meta.env.VITE_API_BASEURL;

const useFetch = <T>(url: string, request: RequestInit) => {
  const [data, setData] = useState<T | null>(null);
  const [error, setError] = useState<Error | null>(null);
  const [loading, setLoading] = useState(true);

  const fetchData = useCallback((url: string, req: RequestInit) => {
    setLoading(true);

    fetch(`${baseUrl}${url}`, req)
      .then((response) => response.json())
      .then((data) => setData(data))
      .catch((error) => {
        console.error('Error fetching data: ', error);
        setError(error);
      })
      .finally(() => setLoading(false));
  }, []);

  useEffect(() => {
    fetchData(url, request);
  }, [fetchData, url, request]);

  return { data, error, loading, fetchData };
};

export default useFetch;
