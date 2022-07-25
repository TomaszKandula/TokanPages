import * as React from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom"; 
import Fab from "@material-ui/core/Fab";
import KeyboardArrowUpIcon from "@material-ui/icons/KeyboardArrowUp";
import { MainPage } from "./Pages";
import { StoryPage } from "./Pages"; 
import { ArticlesPage } from "./Pages";
import { TermsPage } from "./Pages";
import { PolicyPage } from "./Pages";
import { ContactPage } from "./Pages";
import { SigninPage } from "./Pages";
import { SignupPage } from "./Pages";
import { SignoutPage } from "./Pages";
import { AccountPage } from "./Pages";
import { ResetPasswordPage } from "./Pages";
import { UpdatePasswordPage } from "./Pages";
import { UnsubscribePage } from "./Pages";
import { UpdateSubscriberPage } from "./Pages";
import { ActivationPage } from "./Pages";
import { WrongPage } from "./Pages";
import { ScrollToTop } from "./Shared/Components/Scroll";
import { ApplicationToast } from "./Shared/Components";
import { ApplicationDialogBox } from "./Shared/Components";
import { ApplicationUserInfo } from "./Shared/Components";
import { StoreUserData } from "./Shared/Services/updateUserDataService";
import { AppStyle } from "./app.style";
import AOS from "aos";
import "aos/dist/aos.css";

const App = (): JSX.Element => 
{
    const classes = AppStyle();

    AOS.init();
    StoreUserData();

    React.useEffect(() => 
    {
        const intervalId = setInterval(() => AOS.refresh(), 900);
        return(() => clearInterval(intervalId));
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
                  <Route exact path="/contact"><ContactPage /></Route>
                  <Route exact path="/signin"><SigninPage /></Route>
                  <Route exact path="/signup"><SignupPage /></Route>
                  <Route exact path="/signout"><SignoutPage /></Route>
                  <Route exact path="/account"><AccountPage /></Route>
                  <Route exact path="/resetpassword"><ResetPasswordPage /></Route>
                  <Route exact path="/updatepassword"><UpdatePasswordPage /></Route>
                  <Route exact path="/unsubscribe"><UnsubscribePage /></Route>
                  <Route exact path="/updatesubscriber"><UpdateSubscriberPage /></Route>
                  <Route exact path="/accountactivation"><ActivationPage /></Route>
                  <Route exact path="/albums" component={() => //TODO: remove when Gallery is created
                    { window.location.href = "https://500px.com/p/tomaszkandula?view=galleries"; return null; }} />
                  <Route component={WrongPage} />
                </Switch>
            </Router>
            <ApplicationToast />
            <ApplicationDialogBox />
            <ApplicationUserInfo />
            <ScrollToTop>
                <Fab size="small" aria-label="scroll back to top" className={classes.button}>
                    <KeyboardArrowUpIcon/>
                </Fab>
            </ScrollToTop>
        </>
    );
}

export default App;
