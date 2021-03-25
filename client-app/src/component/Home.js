import React, {Component} from 'react';
import axios from 'axios';
//import './Home.css';

export default class Home extends Component{
    state = { 
       
    };
   
    /*componentDidMount(){
        
        axios.get("http://localhost:5000/Passenger").then
            res => {
                this.setState({
                    user: res.data
                })
            }
            err => {
                console.log(err)
            }
    }*/

    render(){

        /*if (this.User!= null){
            return(
                <h2> Hi {this.state.user.Email} ,you log as {this.state.user.role}</h2>
            )
        }*/
        return(
            <h2>THIS IS HOME PAGE</h2>
        )
    }

    
}
