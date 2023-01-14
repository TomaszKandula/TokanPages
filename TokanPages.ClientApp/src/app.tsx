import * as React from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom"; 
import Fab from "@material-ui/core/Fab";
import KeyboardArrowUpIcon from "@material-ui/icons/KeyboardArrowUp";
import { IGetContentManifestDto } from "./Api/Models";
import { ScrollToTop } from "./Shared/Components/Scroll";
import { UpdateUserData } from "./Shared/Services/updateUserDataService";
import { UpdateUserLanguage } from "./Shared/Services/updateUserLanguageService";
import { AppStyle } from "./app.style";
import AOS from "aos";
import "aos/dist/aos.css";

import { 
    MainPage, 
    StoryPage,
    ArticlesPage,
    TermsPage,
    PolicyPage,
    ContactPage,
    SigninPage,
    SignupPage,
    SignoutPage,
    AccountPage,
    ResetPasswordPage,
    UpdatePasswordPage,
    UnsubscribePage,
    UpdateSubscriberPage,
    ActivationPage,
    WrongPage
} from "./Pages";

import { 
    ApplicationToast, 
    ApplicationDialogBox, 
    ApplicationUserInfo 
} from "./Shared/Components";

interface IApp 
{
    manifest: IGetContentManifestDto;
}

const App = (props: IApp): JSX.Element => 
{
    const classes = AppStyle();

    AOS.init();
    UpdateUserData();
    UpdateUserLanguage(props.manifest);

    React.useEffect(() => 
    {
        const intervalId = setInterval(() => AOS.refresh(), 900);
        return(() => clearInterval(intervalId));
    });

    const redirect500px = React.useCallback(() => 
    { 
        window.location.href = "https://500px.com/p/tomaszkandula?view=galleries"; return null; 
    }, 
    [ ]);

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
                  <Route exact path="/albums" component={redirect500px} />
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
