import logo from "./../../logo.svg";
import './Pass_Info_Update.css';
//import Pass_Info_Update from './component/pass_info_update/Pass_Info_Update';
import "bootstrap/dist/css/bootstrap.min.css";

function Pass_Info_Update() {
  return (
    <div class="container p-1">
      <div class="box">
        <h1>
          <u>Passenger Information Update</u>
        </h1>
        <form class="m-5">
          <p>Please fill in this form to create an account!</p>
          <hr />

          <div class="form-group">
            <div class="row">
              <div class="col">
                <label>NIC : </label>
                <label>123456789v</label>
              </div>
            </div>
          </div>
          <div class="form-group">
            <div class="row">
              <div class="col">
                <input
                  type="text"
                  class="form-control"
                  name="first_name"
                  placeholder="First Name"
                  required="required"
                />
              </div>
              <div class="col">
                <input
                  type="text"
                  class="form-control"
                  name="last_name"
                  placeholder="Last Name"
                  required="required"
                />
              </div>
            </div>
          </div>
          <div class="form-group">
            <input
              type="email"
              class="form-control"
              name="email"
              placeholder="Email"
              required="required"
            />
          </div>
          <div class="form-group">
            <input
              type="password"
              class="form-control"
              name="password"
              placeholder="Password"
              required="required"
            />
          </div>
          <div class="form-group">
            <input
              type="Telephone"
              class="form-control"
              name="Telephone"
              placeholder="Telephone"
              required="required"
            />
          </div>
          <div class="form-group"></div>
          <div class="form-group">
            <button type="Update" class="btn btn-primary btn-lg">
              Update
            </button>
          </div>
        </form>
        <div class="hint-text">
          {" "}
          <a href="#"></a>
        </div>
      </div>
    </div>
  );
}

export default Pass_Info_Update;
