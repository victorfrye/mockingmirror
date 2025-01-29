import { useState, useEffect, useRef, useCallback } from 'react';
import axios, { AxiosError, AxiosRequestConfig } from 'axios';

axios.defaults.baseURL = import.meta.env.VITE_API_HTTPS_BASEURL;

// eslint-disable-next-line @typescript-eslint/no-explicit-any
const useAxios = <T, D = any>(request: AxiosRequestConfig<D>) => {
  const [response, setResponse] = useState<T | null>(null);
  const [error, setError] = useState<AxiosError | null>(null);
  const [loading, setLoading] = useState(true);

  const controller = useRef(new AbortController());
  const cancelRequest = () => controller.current.abort();

  const fetchData = useCallback((req: AxiosRequestConfig<D>) => {
    axios
      .request<T>({ ...req, signal: controller.current.signal })
      .then((res) => setResponse(res.data))
      .catch((err) => {
        console.log(err);
        setError(err);
      })
      .finally(() => setLoading(false));
  }, []);

  useEffect(() => {
    fetchData(request);
  }, [fetchData, request]);

  return { response, error, loading, cancelRequest };
};

export default useAxios;
