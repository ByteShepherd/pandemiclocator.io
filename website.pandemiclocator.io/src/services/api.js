import axios from 'axios';

const api = axios.create({
    baseURL: "https://localhost:32770"
})

export default api;