import * as React from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
  
import MainPage from "./Pages/mainPage";
import MyStory from "./Pages/myStory"; 

export default function App() 
{

    return (
        <Router>
            <Switch>
              <Route exact path="/"><MainPage /></Route>
              <Route exact path="/mystory"><MyStory /></Route>
            </Switch>
        </Router>
    );

};
