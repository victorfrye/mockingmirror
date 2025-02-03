import { Reducer, useCallback, useEffect, useReducer } from 'react';

const baseUrl = import.meta.env.VITE_API_BASEURL;

interface FetchState<T> {
  data: T | null;
  error: Error | null;
  loading: boolean;
}

type FetchAction<T> =
  | { type: 'INIT' }
  | { type: 'SUCCESS'; payload: T }
  | { type: 'FAILURE'; payload: Error };

const reducer = <T>(
  state: FetchState<T>,
  action: FetchAction<T>
): FetchState<T> => {
  switch (action.type) {
    case 'INIT':
      return { ...state, loading: true, error: null };
    case 'SUCCESS':
      return { ...state, loading: false, data: action.payload };
    case 'FAILURE':
      return { ...state, loading: false, error: action.payload };
    default:
      throw new Error();
  }
};

const useFetch = <T>(url: string, request: RequestInit) => {
  const [state, dispatch] = useReducer<Reducer<FetchState<T>, FetchAction<T>>>(
    reducer,
    {
      data: null,
      error: null,
      loading: true,
    }
  );

  const fetchData = useCallback((url: string, req: RequestInit) => {
    dispatch({ type: 'INIT' });

    fetch(`${baseUrl}${url}`, req)
      .then((response) =>
        response.ok
          ? response.json()
          : Promise.reject(
              new Error('Network response was not OK ' + response.statusText)
            )
      )
      .then((data) => dispatch({ type: 'SUCCESS', payload: data }))
      .catch((error) => {
        console.error('Error fetching data: ', error);
        dispatch({ type: 'FAILURE', payload: error });
      });
  }, []);

  useEffect(() => {
    fetchData(url, request);
  }, [fetchData, url, request]);

  return state;
};

export default useFetch;
