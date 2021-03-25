import axios from 'axios';
import authHeader from './auth-header';

const API_URL = 'http://localhost:5000/';

class UserService {
  
  getUserBoard() {
    return axios.get(API_URL + 'Passenger', { headers: authHeader() });
  }

  getBusControllerBoard() {
    return axios.get(API_URL + 'BusInfo', { headers: authHeader() });
  }
/*
  getAdminBoard() {
    return axios.get(API_URL + 'Admin', { headers: authHeader() });
  }*/
}

export default new UserService();
