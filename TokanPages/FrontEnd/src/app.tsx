import * as React from "react";
import {
    BrowserRouter as Router,
    Switch,
    Route
} from "react-router-dom";
  
import IndexPage from "./pages/index";
import MystoryPage from "./pages/mystory"; 

export default function App() 
{

    return (
        <Router>
            <Switch>
              <Route exact path="/"><IndexPage /></Route>
              <Route exact path="/mystory"><MystoryPage /></Route>
            </Switch>
        </Router>
    );

};
