import * as React from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom"; 
import Fab from "@material-ui/core/Fab";
import KeyboardArrowUpIcon from "@material-ui/icons/KeyboardArrowUp";
import MainPage from "./Pages/mainPage";
import StoryPage from "./Pages/storyPage"; 
import ArticlesPage from "./Pages/articlesPage";
import TermsPage from "./Pages/termsPage";
import PolicyPage from "./Pages/policyPage";
import SigninPage from "./Pages/signinPage";
import SignupPage from "./Pages/signupPage";
import ResetPage from "./Pages/resetPage";
import UnsubscribePage from "./Pages/unsubscribePage";
import ScrollTop from "./Shared/Scroll/scrollTop";
import AOS from "aos";
import "aos/dist/aos.css";

export default function App() 
{

    React.useEffect(() => 
    {
        AOS.init();
        AOS.refresh();
    });

    return (
        <>
            <Router>
                <Switch>
                  <Route exact path="/"><MainPage /></Route>
                  <Route exact path="/mystory"><StoryPage /></Route>
                  <Route exact path="/articles"><ArticlesPage /></Route>
                  <Route exact path="/terms"><TermsPage /></Route>
                  <Route exact path="/policy"><PolicyPage /></Route>
                  <Route exact path="/signin"><SigninPage /></Route>
                  <Route exact path="/signup"><SignupPage /></Route>
                  <Route exact path="/reset"><ResetPage /></Route>
                  <Route exact path="/unsubscribe"><UnsubscribePage /></Route>
                </Switch>
            </Router>
            <ScrollTop>
                <Fab color="primary" size="small" aria-label="scroll back to top">
                    <KeyboardArrowUpIcon/>
                </Fab>
            </ScrollTop>
      </>
    );

};
