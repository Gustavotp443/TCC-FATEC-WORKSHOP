import axios from "axios";


const api = axios.create({
  baseURL: "https://localhost:7186/"
});


export default api;