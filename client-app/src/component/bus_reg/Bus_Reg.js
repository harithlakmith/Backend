import "bootstrap/dist/css/bootstrap.min.css";
import "./Bus_Reg.css";
import "bootstrap/dist/css/bootstrap.min.css";
import React from "react";
import axios from "axios";

class Bus_Reg extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      busNo: "",
      email: "",
      password: "",
      seatNo: 0,
      dName: "",
      dNo: 0,
      cName: "",
      cNo: 0,
    };
    this.handleChange = this.handleChange.bind(this);
    this.BusReg = this.BusReg.bind(this);
  }

  handleChange = (e) => {
    this.setState({ [e.target.name]: e.target.value });
  };

  BusReg = () => {
    axios
      .post("http://localhost:5000/api/Accounts/BusControllerRegister", {
        BusNo:this.state.busNo,
        DriverName: this.state.dName,
        DriverNo: parseInt(this.state.dNo),
        CondName: this.state.cName,
        CondNo: parseInt(this.state.cNo),
        MaxSeats: parseInt(this.state.seatNo),
        Email: this.state.email,
        Password: this.state.password,
        Verified:1
      })
      .then((json) => {
        console.log(json.data);
      });
  };

  render() {
    return (
      <div class="container p-1">
        <div class="box">
          <h1>
            <u>Bus Registration Form</u>
          </h1>

          <br></br>
          <br></br>
          <form>
          <div class="row">
            <div class="box-box1"></div>
            <div class="box-box4">
              <br></br>
              <br></br>
              <div class="row">
                <div class="col-lg-4 ; h5 ">Bus Number</div>
                <div class="col-lg-5">
                  <input
                    class="form"
                    type="text"
                    name="busNo"
                    placeholder="add bus number"
                    onChange={this.handleChange}
                    value={this.state.busNo}
                    required="required"
                  ></input>
                </div>
              </div>
              <br></br>
              <div class="row">
                <div class="col-lg-4 ; h5 ">Add E-Mail</div>
                <div class="col-lg-5">
                  <input
                    class="form"
                    type="text"
                    name="email"
                    placeholder="add your email"
                    onChange={this.handleChange}
                    value={this.state.email}
                    required="required"
                  ></input>
                </div>
              </div>
              <br></br>
              <div class="row">
                <div class="col-lg-4 ; h5 ">Password</div>
                <div class="col-lg-5">
                  <input
                    class="form"
                    type="password"
                    name="password"
                    placeholder="add your password"
                    onChange={this.handleChange}
                    value={this.state.password}
                    required="required"
                  ></input>
                </div>
              </div>
              <br></br>
              
              <div class="row">
                <div class="col-lg-4 ; h5 ">Number of Seats</div>
                <div class="col-lg-1">
                  <input
                    type="text"
                    pattern="[0-9]*"
                    min="1"
                    max="54"
                    name="seatNo"
                    onChange={this.handleChange}
                    value={this.state.seatNo}
                    required="required"
                  ></input>
                </div>
              </div>
              <br></br>
            </div>
            <br></br>
            <div class="box-box2">
              <div class="row; h3">Driver</div>
              <br></br>
              <div class="row">
                <div class="col-lg-2"></div>
                <div class="col-lg-4 ; h5 ">Name</div>
                <div class="col-lg-5">
                  <input
                    class="form"
                    type="text"
                    name="dName"
                    onChange={this.handleChange}
                    value={this.state.dName}
                  ></input>
                </div>
              </div>
              <br></br>
              <div class="row">
                <div class="col-lg-2"></div>
                <div class="col-lg-4 ; h5 ">Register No.</div>
                <div class="col-lg-5">
                  <input
                    class="form"
                    type="text"
                    pattern="[0-9]*"
                    name="dNo"
                    onChange={this.handleChange}
                    value={this.state.dNo}
                    required="required"
                  ></input>
                </div>
              </div>
              <br></br>
              <br></br>
              <div class="row; h3">Conductor</div>
              <br></br>
              <div class="row">
                <div class="col-lg-2"></div>
                <div class="col-lg-4 ; h5 ">Name</div>
                <div class="col-lg-5">
                  <input
                    class="form"
                    type="text"
                    name="cName"
                    onChange={this.handleChange}
                    value={this.state.cName}
                  ></input>
                </div>
              </div>
              <br></br>
              <div class="row">
                <div class="col-lg-2"></div>
                <div class="col-lg-4 ; h5 ">Register No.</div>
                <div class="col-lg-5">
                  <input
                    class="form"
                    type="text"
                    pattern="[0-9]*"
                    name="cNo"
                    onChange={this.handleChange}
                    value={this.state.cNo}
                    required="required"
                  ></input>
                </div>
              </div>
            </div>
          </div></form>
          <br></br>
          <br></br>
          <div class="row">
            <div class="box-box1"></div>
            <button type="button" onClick={this.BusReg} class="btn btn-primary btn-lg">
              REGISTER
            </button>
          </div>
        </div>
      </div>
    );
  }
}

export default Bus_Reg;
