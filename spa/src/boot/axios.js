import Vue from 'vue'
import axios from 'axios'
import { ROUTING } from '../constants/routing';

Vue.prototype.$axios = axios

axios.interceptors.request.use((config) => {
  config.mode = "cors";
  config.url = ROUTING.baseRoute + config.url;
  return config;
}, function (error) {
  return Promise.reject(error);
});