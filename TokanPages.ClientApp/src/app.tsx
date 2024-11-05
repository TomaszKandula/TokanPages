import * as React from "react";
import { BrowserRouter as Router, Switch } from "react-router-dom";
import Fab from "@material-ui/core/Fab";
import KeyboardArrowUpIcon from "@material-ui/icons/KeyboardArrowUp";
import { GetContentManifestDto } from "./Api/Models";
import { UpdateUserData } from "./Shared/Services/initializeService";
import { UpdateUserLanguage } from "./Shared/Services/languageService";
import { AppStyle } from "./app.style";
import { Routes } from "./routes";
import AOS from "aos";
import "aos/dist/aos.css";
import {
    ClearPageStart,
    ScrollToTop,
    ApplicationToast,
    ApplicationDialogBox,
    ApplicationUserInfo,
    ApplicationSession,
} from "./Shared/Components";

interface Properties {
    manifest: GetContentManifestDto;
}

const App = (props: Properties): React.ReactElement => {
    const classes = AppStyle();
    const queryParam = new URLSearchParams(window.location.search);
    const mode = queryParam.get("mode");
    const isStatic = mode === "static";

    UpdateUserData();
    UpdateUserLanguage(props.manifest);

    React.useEffect(() => {
        AOS.init({ once: !isStatic, disable: isStatic });
        const intervalId = setInterval(() => AOS.refresh(), 900);
        return () => clearInterval(intervalId);
    }, [isStatic]);

    return (
        <ApplicationSession>
            <Router>
                <ClearPageStart>
                    <Switch>
                        <Routes />
                    </Switch>
                </ClearPageStart>
            </Router>
            <ApplicationToast />
            <ApplicationDialogBox />
            <ApplicationUserInfo />
            <ScrollToTop>
                <Fab size="small" aria-label="scroll back to top" className={classes.button}>
                    <KeyboardArrowUpIcon />
                </Fab>
            </ScrollToTop>
        </ApplicationSession>
    );
};

export default App;
