import logo from './logo.svg';
//import './App.css';
import {BrowserRouter, Route, Switch} from 'react-router-dom';
import Pass_Info_Update from './component/pass_info_update/Pass_Info_Update';
//import Pass_Login from './component/pass_login/Pass_Login';
import Pass_Reg_Form from './component/pass_reg_form/Pass_Reg_Form';
//import './component/signin/node_modules/bootstrap/dist/css/bootstrap.min.css';
import Home from './component/Home';
import { render } from 'react-dom';
import React, {Component} from 'react';
import SignIn from './component/signin/SignIn';
import Bus_Reg from './component/bus_reg/Bus_Reg';


class App extends Component{
 

 
render()

  {

  return (
    <BrowserRouter>
    
    <div className="App" >
     

      <div>
        <div>
          <Switch>
            <Route exact path = "/" component={Home}/>
            
            <Route exact path = "/pass-Reg" component={Pass_Reg_Form}/>
            <Route exact path = "/signin" component={SignIn}/>
            <Route exact path = "/busreg" component={Bus_Reg}/>
            

          </Switch>
          
        </div>
      </div>

      
      
    </div>
    </BrowserRouter>
      
  );
  
}
}

export default App;
