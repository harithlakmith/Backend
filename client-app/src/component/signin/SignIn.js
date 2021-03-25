import logo from './../../logo.svg';
import './SignIn.css';
import React, {Component} from 'react';
import axios from 'axios';
//import Pass_Info_Update from './component/pass_info_update/Pass_Info_Update';
import 'bootstrap/dist/css/bootstrap.min.css';
import {Redirect} from 'react-router-dom';

class SignIn extends Component{

    state = {
        errorMessage: ''
        

    }

    submitHandler = e => {
        e.preventDefault()
        
        const data ={
            Email: this.email,
            Password: this.password
        }

        console.log(data)

        axios
			.post("http://localhost:5000/api/Accounts/Login", data)
			.then(response => {
                console.log(response)
                localStorage.setItem('user', response.data)
                this.setState({
                    loggedIn: true
                })
                this.props.setUser(response.data)

                /*
                if(response.status == 200){
                    alert("Loggin Succesful!")
                }
                */
			})
			.catch(error => {
                console.log(error)
                //localStorage.setItem('user', this.state.temp)
                this.setState({
                    loggedIn: false
                })
                //this.setState({errorMessage: 'Check your email and password!(' +error.message+ ')'})
                
                
			})
    }




render(){
    if (this.state.loggedIn){
        return <Redirect to={'/'} />
    }
    
    
    return (
      <form onSubmit = {this.submitHandler}>
      <div className="container p-1">
        <div className="box">
          <h1>
            <u>Passenger Login</u>
          </h1>
             
      
            <hr/>
  
          <div className="row">
              <div className="col-md-6">
              <div  >
          
          
         
          
          
        
          <div className="form-group">
          <input type="text" className="form-control" name="email"  onChange={e =>this.email = e.target.value}  placeholder="Email" required="required"></input>
          </div>
          <div className="form-group">
              <input type="password" className="form-control" name="password"  onChange={e =>this.password = e.target.value} placeholder="Password" required="required"></input>
          </div>   
          
      </div>
  
      <div className="form-group">
              <button type="submit" className="btn btn-primary btn-lg">Login</button>
          </div>
              </div>
              <div className="col-md-6">
              <img src="./../../login.JPG" alt="login" className="center"/>
              </div>
  
             
          </div>
  
  
  </div>
  
  </div>
   </form>   
    );
  }
  }
  
  export default SignIn;