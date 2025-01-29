import { useState, useEffect, useRef, useCallback } from 'react';
import axios, { AxiosError, AxiosRequestConfig } from 'axios';

axios.defaults.baseURL = import.meta.env.VITE_API_HTTPS_BASEURL;

const useAxios = <T, R>(request: AxiosRequestConfig<T>) => {
  const [response, setResponse] = useState<R | null>(null);
  const [error, setError] = useState<AxiosError | null>(null);
  const [loading, setLoading] = useState(true);

  const controller = useRef(new AbortController());
  const cancelRequest = () => controller.current.abort();

  const fetchData = useCallback((req: AxiosRequestConfig<T>) => {
    axios
      .request<R>({ ...req, signal: controller.current.signal })
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
