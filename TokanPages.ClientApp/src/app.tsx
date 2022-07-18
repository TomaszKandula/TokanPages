import * as React from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom"; 
import Fab from "@material-ui/core/Fab";
import KeyboardArrowUpIcon from "@material-ui/icons/KeyboardArrowUp";
import MainPage from "./Pages/MainPage";
import StoryPage from "./Pages/StoryPage"; 
import ArticlesPage from "./Pages/ArticlesPage";
import TermsPage from "./Pages/TermsPage";
import PolicyPage from "./Pages/PolicyPage";
import ContactPage from "./Pages/ContactPage";
import SigninPage from "./Pages/SigninPage";
import SignupPage from "./Pages/SignupPage";
import SignoutPage from "./Pages/SignoutPage";
import AccountPage from "./Pages/AccountPage";
import ResetPasswordPage from "./Pages/ResetPasswordPage";
import UpdatePasswordPage from "./Pages/UpdatePasswordPage";
import UnsubscribePage from "./Pages/UnsubscribePage";
import UpdateSubscriberPage from "./Pages/UpdateSubscriberPage";
import ActivationPage from "./Pages/ActivationPage";
import WrongPage from "./Pages/WrongPage";
import ScrollTop from "./Shared/Components/Scroll/scrollTop";
import ApplicationToast from "./Shared/Components/Toasts/applicationToast";
import ApplicationDialogBox from "./Shared/Components/ApplicationDialogBox/applicationDialogBox";
import ApplicationUserInfo from "./Shared/Components/ApplicationUserInfo/applicationUserInfo";
import { StoreUserData } from "./Shared/Services/updateUserDataService";
import styles from "./Styles/appStyle";
import AOS from "aos";
import "aos/dist/aos.css";

const App = (): JSX.Element => 
{
    const classes = styles();

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
            <ScrollTop>
                <Fab size="small" aria-label="scroll back to top" className={classes.button}>
                    <KeyboardArrowUpIcon/>
                </Fab>
            </ScrollTop>
        </>
    );
}

export default App;
