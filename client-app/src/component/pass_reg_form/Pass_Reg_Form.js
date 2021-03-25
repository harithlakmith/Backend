import React from 'react'
import 'bootstrap/dist/css/bootstrap.min.css';
import "./Pass_Reg_Form.css";

import axios from 'axios';

 class Pass_Reg_Form extends React.Component {

	constructor(props){  
		super(props); 
		this.state = {
		  
		  NIC:'',
		  First_name:'',
		  Last_name:'',
		  Email:'',
		  Password:'',
		  ConfirmPassword:'',
		  Telephone:''
	
		};
	    this.handleChange = this.handleChange.bind(this);
		  this.AddPass = this.AddPass.bind(this);
	  }
	  
		handleChange = (e) => {
		  this.setState({ [e.target.name]: e.target.value});
		};
	  
		AddPass = () => {
		  //event.preventDefault();
	
		  axios.post('http://localhost:5000/api/Accounts/PassengerRegister', {
			//PID: parseInt(this.state.NIC),
			NIC : this.state.NIC,
			FirstName: this.state.First_name,
			LastName: this.state.Last_name,
			Email: this.state.Email,
			Password: this.state.Password,
			//ConfirmPassword: this.state.ConfirmPassword,
			Tp: parseInt(this.state.Telephone),
			Token : 'test',
			Verified:0
	
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
			  <u>Passenger Registration Form</u>
			</h1>
	
		<div className="Pass_Reg_Form">
		  <form>
			
			<p></p>
			<hr/>
			<div class="form-group">
	
			  <div class="form-group">
			    <input 
          type="text" 
          class="form-control" 
          name="NIC" 
          onChange={this.handleChange} 
          value={this.state.NIC} 
          placeholder="NIC" 
          required="required"></input>
			  </div>
				<div class="row">
					<div class="col">
            <input 
            type="text" 
            class="form-control" 
            name="First_name" 
            onChange={this.handleChange} 
            value={this.state.First_name} 
            placeholder="First Name" 
            required="required"></input>
          </div>
					<div class="col">
            <input 
            type="text" 
            class="form-control" 
            name="Last_name" 
            onChange={this.handleChange} 
            value={this.state.Last_name} 
            placeholder="Last Name" 
            required="required"></input>
          </div>
				</div>        	
			</div>
			<div class="form-group">
				<input 
        type="text" 
        class="form-control" 
        name="Email" 
        onChange={this.handleChange} 
        value={this.state.Email} 
        placeholder="Email" 
        required="required"></input>
			</div>
			<div class="form-group">
				<input 
        type="password" 
        class="form-control" 
        name="Password" 
        onChange={this.handleChange} 
        value={this.state.Password} 
        placeholder="Password" 
        required="required"></input>
			</div>
			
			<div class="form-group">
				<input 
        type="text" 
        pattern="[0-9]*" 
        class="form-control" 
        onChange={this.handleChange} 
        value={this.state.Telephone} 
        name="Telephone" 
        placeholder="Telephone" 
        required="required"></input>
			</div> </form>       
			<div class="form-group">
				
			</div>
			<div class="form-group">
				<button 
        type="submit" 
        onClick={this.AddPass} 
        class="btn btn-primary btn-lg">
          Register
        </button>
			</div>
		
		</div>
		</div>
		</div>
	  );
	  }
   
}
export default Pass_Reg_Form 