import axios from 'axios';

const getBaseUrl = () => {
  switch (import.meta.env.NODE_ENV) {
    case 'production':
      return import.meta.env.VITE_APP_API_BASE_URL_PROD;
    case 'development':
    default:
      return import.meta.env.VITE_APP_API_BASE_URL_DEV;
  }
};

export const axiosClient = axios.create({
  baseURL: getBaseUrl(),
});
