import axios from "axios";

const API_URL = "http://localhost:5000/api/Accounts/";

class AuthService {
  login(Email,Password) {
    return axios
      .post(API_URL + "Login", {
        Email,
        Password
      })
      .then(response => {
        if (response.data.accessToken) {
          localStorage.setItem("user", JSON.stringify(response.data));
        }

        return response.data;
      });
  }

  logout() {
    localStorage.removeItem("user");
  }

  PassReg( NIC,FirstName,LastName,Email,Password,/*ConfirmPassword,*/Tp,Token,Verified) {
    return axios.post(API_URL + "PassengerRegister", {
        NIC,
        FirstName,
        LastName,
        Email,
        Password,
        //ConfirmPassword,
        Tp,
        Token,
        Verified
    });
  }

  BusReg( BusNo,DriverName,DriverNo,CondName,CondNo,MaxSeats,Email,Password,Verified) {
    return axios.post(API_URL + "BusControllerRegister", {
        BusNo,
        DriverName,
        DriverNo,
        CondName,
        CondNo,
        MaxSeats,
        Email,
        Password,
        Verified
    });
  }

  getCurrentUser() {
    return JSON.parse(localStorage.getItem('user'));;
  }
}

export default new AuthService();
